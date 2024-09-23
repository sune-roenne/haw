using ThousandAcreWoods.Language.Extensions;
using System.Collections.Immutable;
using ThousandAcreWoods.AudioBook.TextToSpeech.Model;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
internal record SsmlGenerationState(
    ImmutableStack<SsmlGenerationElement> OpenElements,
    IEnumerable<SsmlGenerationToken> GeneratedTokens,
    ImmutableStack<SsmlGenerationElement> ToReopen
    )
{
    private readonly List<SsmlGenerationToken> _tokens = new List<SsmlGenerationToken>(GeneratedTokens);
    private IReadOnlyCollection<SsmlGenerationToken> CopyOpens => _tokens.ToReadonlyCollection();
    public static SsmlGenerationState Start() => (new SsmlGenerationRootElement())
        .Pipe(root => new SsmlGenerationState([root], [SsmlGenerationToken.Open(root)], []));

    public SsmlGenerationState WithVoice(TextToSpeechVoiceDefinition voice) 
    {
        if (IsAlreadySatisfied((elem, _) => elem is SsmlGenerationVoiceElement voi && voi.Voice.ShortName == voice.ShortName))
            return this;
        InsertOnTopOf<SsmlGenerationRootElement, SsmlGenerationVoiceElement>(
            elementProducer: _ => new SsmlGenerationVoiceElement(voice), 
            out var openElements, out var toReopen);
        return new SsmlGenerationState(OpenElements: openElements, CopyOpens, ToReopen: toReopen);
    }

    public SsmlGenerationState WithExpressAs(TextToSpeechVoiceConfiguration? config)
    {
        if (config == null || (config.Style == null))
            return this;
        var toCreate = new SsmlGenerationExpressAsElement(config.Style, 1.5m, config.Role?.Pipe(_ => _.ToString()).PipeOpt(_ => _.Length > 3 ? _ : null));
        if (IsAlreadySatisfied((elem, level) => elem is SsmlGenerationExpressAsElement exp && exp.IsSameAs(toCreate)))
            return this;

        InsertOnTopOf<SsmlGenerationVoiceElement, SsmlGenerationExpressAsElement>(
            elementProducer: _ => toCreate,
            out var openElements, out var toReopen);
        return new SsmlGenerationState(OpenElements: openElements, CopyOpens, ToReopen: toReopen);
    }


    public SsmlGenerationState WithParagraph()
    {
        if (IsAlreadySatisfied((topmost, level) => level == 0 && topmost is SsmlGenerationParagraphElement))
            return this;
        InsertOnTopOf(
            elementProducer: topmost => new SsmlGenerationParagraphElement(topmost.NodeLevel + 1),
            parentCondition: possible => possible.GetType() != typeof(SsmlGenerationParagraphElement) && possible.GetType() != typeof(SsmlGenerationSentenceElement),
            out var openElements,
            out var toReopen);
        return new SsmlGenerationState(OpenElements: openElements, CopyOpens, ToReopen: toReopen);
    }

    public SsmlGenerationState WithSentence(string sentenceText)
    {
        InsertOnTopOf(
            elementProducer: topmost => new SsmlGenerationSentenceElement(topmost.NodeLevel + 1, sentenceText),
            parentCondition: possible => possible.GetType() != typeof(SsmlGenerationSentenceElement),
            out var openElements,
            out var toReopen);
        return new SsmlGenerationState(OpenElements: openElements, CopyOpens, ToReopen: toReopen);
    }

    public SsmlGenerationState WithProsody(TextToSpeechVoiceConfiguration? conf)
    {
        if (conf == null || (conf.ContourChanges == null && conf.PitchChangeInPercent == null && conf.RateInPercent == null && conf.PitchInHertz == null))
            return this;

        if (IsAlreadySatisfied((topmost, level) => topmost is SsmlGenerationProsodyElement pros && pros.IsSameAs(new SsmlGenerationProsodyElement(1, conf))))
            return this;

        InsertOnTopOf(
            elementProducer: topmost => new SsmlGenerationProsodyElement(topmost.NodeLevel + 1, conf),
            parentCondition: possible => true,
            out var openElements,
            out var toReopen);
        return new SsmlGenerationState(OpenElements: openElements, CopyOpens, ToReopen: toReopen);
    }


    public SsmlGenerationState WithPause(int pauseInMilliseconds)
    {
        InsertOnTopOf(
            elementProducer: topmost => topmost switch
            {
                _ => new SsmlGenerationPauseElement(topmost.NodeLevel + 1 ,pauseInMilliseconds)
            },
            parentCondition: possible => true,
            out var openElements,
            out var toReopen);
        return new SsmlGenerationState(OpenElements: openElements, CopyOpens, ToReopen: toReopen);
    }


    public SsmlGenerationState WithClosedOpens()
    {
        if (ToReopen.IsEmpty)
            return this;
        var topmost = ToReopen.Peek();
        var newOpens = topmost switch
        {
            SsmlGenerationExpressAsElement exp => ReopenAtParent<SsmlGenerationVoiceElement>(),
            SsmlGenerationParagraphElement par => ReopenToWhere(elem => SsmlGenerationParagraphElement.PossibleParents.Contains(elem.GetType())),
            SsmlGenerationVoiceElement voic => ReopenAtParent<SsmlGenerationVoiceElement>(),
            _ => ReopenToWhere(_ => true)
        };
        return new SsmlGenerationState(OpenElements: newOpens, CopyOpens, []);
    }

    public string Complete()
    {
        var opens = OpenElements;
        while (!opens.IsEmpty)
        {
            opens = opens.Pop(out var next);
            Close(next);
        }
        var tokens = Reduce(_tokens);
        var returnee = tokens
            .Select(_ => _.ToString())
            .MakeString("\r\n");
        return returnee;
    }



    private static IReadOnlyCollection<SsmlGenerationToken> Reduce(IEnumerable<SsmlGenerationToken> tokens)
    {
        var tokenList = tokens.ToList();
        var returnee = new List<SsmlGenerationToken>();
        while(tokenList.Any())
        {
            tokenList = ReduceToComplete<SsmlGenerationPauseElement>(tokenList, out var didReduce);
            if (didReduce) continue;
            tokenList = ReduceToComplete<SsmlGenerationSentenceElement>(tokenList, out didReduce);
            if (didReduce) continue;

            tokenList = SkipEmpty<SsmlGenerationExpressAsElement>(tokenList, out var didSkip);
            if (didSkip) continue;
            tokenList = SkipEmpty<SsmlGenerationParagraphElement>(tokenList, out didSkip);
            if (didSkip) continue;
            tokenList = SkipEmpty<SsmlGenerationProsodyElement>(tokenList, out didSkip);
            if (didSkip) continue;
            tokenList = SkipEmpty<SsmlGenerationSentenceElement>(tokenList, out didSkip);
            if (didSkip) continue;
            tokenList = SkipEmpty<SsmlGenerationVoiceElement>(tokenList, out didSkip);
            if (didSkip) continue;
            returnee.Add(tokenList[0]);
            tokenList = tokenList.Skip(1).ToList();
        }
        return returnee;
    }

    private static List<SsmlGenerationToken> SkipEmpty<TElem>(List<SsmlGenerationToken> input, out bool didSkip) where TElem : SsmlGenerationElement
    {
        if (input is [SsmlGenerationOpenToken<TElem> op, SsmlGenerationCloseToken<SsmlGenerationElement> clGen, ..] && typeof(TElem) == clGen.Element.GetType())
        {
            didSkip = true;
            return input.Skip(2).ToList();
        }
        else if (input is [SsmlGenerationOpenToken<TElem> opEnd, SsmlGenerationCloseToken<SsmlGenerationElement> clEndGen] && typeof(TElem) == clEndGen.Element.GetType())
        {
            didSkip = true;
            return [];
        }
        didSkip = false;
        return input;
    }

    private static List<SsmlGenerationToken> ReduceToComplete<TElem>(List<SsmlGenerationToken> input, out bool didReduce) where TElem : SsmlGenerationElement
    {
        if (input is [SsmlGenerationOpenToken<TElem> op, SsmlGenerationContentToken<TElem> cont, SsmlGenerationCloseToken<TElem> cls, ..])
        {
            if(op != null && cont != null && cls != null) 
            {
                didReduce = true;
                input = input.Skip(3).ToList();
                input = input.Prepend(SsmlGenerationToken.Complete(op.Element)).ToList();
                return input;
            }

        }
        didReduce = false;
        return input;
    }


    private bool IsAlreadySatisfied(Func<SsmlGenerationElement, int, bool> predicate)
    {
        var opens = OpenElements;
        var levelFromBottom = 0;
        while (!opens.IsEmpty)
        {
            opens = opens.Pop(out var popped);
            if (predicate(popped, levelFromBottom))
                return true;
            levelFromBottom += 1;
        }
        return false;
    }


    private void InsertOnTopOf<TParent, TElem>(
        Func<SsmlGenerationElement, TElem> elementProducer,
        out ImmutableStack<SsmlGenerationElement> openElements,
        out ImmutableStack<SsmlGenerationElement> elementsToReopen) 
           where TParent : SsmlGenerationElement 
           where TElem : SsmlGenerationElement
          => InsertOnTopOf(elementProducer, topmost => topmost is TParent, out openElements, out elementsToReopen); 

    private void InsertOnTopOf<TElem>(
        Func<SsmlGenerationElement, TElem> elementProducer,
        Func<SsmlGenerationElement, bool> parentCondition,
        out ImmutableStack<SsmlGenerationElement> openElements,
        out ImmutableStack<SsmlGenerationElement> elementsToReopen) where TElem : SsmlGenerationElement
    {
        var opens = OpenElements;
        var toReopen = ImmutableStack<SsmlGenerationElement>.Empty;

        while (!opens.IsEmpty)
        {
            var topmost = opens.Peek();
            if (parentCondition(topmost))
            {
                var elem = elementProducer(topmost);
                if (elem.AutoClose)
                {
                    Insert(_tokens,  [SsmlGenerationToken.Open(elem), SsmlGenerationToken.Content(elem), SsmlGenerationToken.Close(elem)]);
                }
                else
                {
                    Open(elem);
                    opens = opens.Push(elem);
                }
                break;

            }
            else
            {
                opens = opens.Pop();
                toReopen = toReopen.Push(topmost);
                Close(topmost);
            }

        }
        if (opens.IsEmpty)
            throw new Exception($"Did not find no parent, stack at start: \r\n{OpenElements.Select(_ => _.OpenTag).MakeString("\r\n")}");
        openElements = opens;
        elementsToReopen = toReopen;
    }


    private ImmutableStack<SsmlGenerationElement> ReopenAtParent<TParent>() where TParent : SsmlGenerationElement => 
        ReopenToWhere(elem => elem is  TParent);

    private ImmutableStack<SsmlGenerationElement> ReopenToWhere(Func<SsmlGenerationElement, bool> parentCondition) 
    {

        var toReopen = ToReopen;
        var currentlyOpen = OpenElements;
        if (!toReopen.IsEmpty)
        {
            while (!currentlyOpen.IsEmpty)
            {
                var topmost = currentlyOpen.Peek();
                if (parentCondition(topmost))
                {
                    while (!toReopen.IsEmpty)
                    {
                        toReopen = toReopen.Pop(out var next);
                        Open(next);
                    }
                }
                else
                {
                    currentlyOpen = currentlyOpen.Pop();
                    Close(topmost);
                }
            }
        }
        if (currentlyOpen.IsEmpty)
            throw new Exception($"Never found the parent to reopen for {toReopen.Peek().OpenTag}  on");
        return currentlyOpen;
    }



    private void Open<TElem>(TElem element) where TElem : SsmlGenerationElement => Insert(_tokens, element, isOpen: true);
    private void Close<TElem>(TElem element) where TElem : SsmlGenerationElement => Insert(_tokens, element, isOpen: false);

    private static void Insert<TElem>(List<SsmlGenerationToken> tokens, TElem elem, bool isOpen) where TElem : SsmlGenerationElement
        => Insert(tokens, [isOpen ? SsmlGenerationToken.Open(elem) : SsmlGenerationToken.Close(elem)]);

    private static void Insert(
        List<SsmlGenerationToken> tokens, 
        IEnumerable<SsmlGenerationToken> tokensToInsert) 
    {
        foreach(var tok in tokensToInsert)
            tokens.Add(tok);
    }



}
