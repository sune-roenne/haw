using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public partial class TextViewerComponent
{
    [CascadingParameter]
    public SiteChapter Chapter { get; set; }

    [CascadingParameter]
    public ReadingPreferences Preferences { get; set; }
    [CascadingParameter]
    public SiteState SiteState { get; set; }

    [Parameter]
    public Action<int> ScrollTo { get; set; }


    private IReadOnlyCollection<DisplayParagraph>? _paragraphs;

    private IReadOnlyCollection<RenderParagraph> RenderParagraphs => (_paragraphs ?? new List<DisplayParagraph>())
        .Select((par, indx) => (Paragraph: par, Index: indx))
        .TakeWhile(_ => _.Index <= SiteState.CurrentParagraphIndex || !SiteState.IsAutomatedReadingEnabled)
        .Select(_ => new RenderParagraph(Paragraph: _.Paragraph, ShouldAnimate: SiteState.IsAutomatedReadingEnabled && _.Index == SiteState.CurrentParagraphIndex))
        .ToList();



    protected override void OnParametersSet()
    {
        _paragraphs = Chapter.ToDisplayModel(Preferences.PreferredFont);
        InvokeAsync(StateHasChanged);
    }

    private bool LeftArrowIsDisabled => SiteState.CurrentChapterIsFirstChapter;
    private bool RightArrowIsDisabled => SiteState.CurrentChapterIsLastChapter;

    private void OnLeftArrowClick() => SiteState.ChangeChapter(SiteState.CurrentChapter - 1);
    private void OnRightArrowClick()
    {
        SiteState.ChangeChapter(SiteState.CurrentChapter + 1);
        _ = Task.Run(async () => 
        {
            await Task.Delay(1000);
            ScrollTo(0);
        });
    }

    private record RenderParagraph(
        DisplayParagraph Paragraph,
        bool ShouldAnimate
        );

    private void OnFinishedRendering()
    {
        if(SiteState.CurrentParagraphIndex > 1)
           ScrollTo(SiteState.CurrentParagraphIndex);
        SiteState.IncrementScrollingStatusFromAnimation();
        if(_paragraphs != null && SiteState.CurrentParagraphIndex >= _paragraphs.Count - 1)
        {
            SiteState.AutomateReading(false);
        }
        SiteState.OnUpdate();
    }

}
