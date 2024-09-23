using System.Collections.ObjectModel;
using System.Text;

namespace ThousandAcreWoods.Language.Extensions;
public static class EnumerableExtensions
{

    public static IReadOnlyCollection<A> ToReadonlyCollection<A>(this IEnumerable<A> enumerable) => enumerable.ToList();
    public static IReadOnlyDictionary<A, B> ToReadOnlyDictionary<A, B>(this IDictionary<A, B> dict) => new ReadOnlyDictionary<A, B>(dict);

    //public static Dictionary<TKey, TEnt> ToDictionarySafe<TEnt, TKey>(this IEnumerable<TEnt> input, Func<TEnt, TKey> keyExtractor) where TKey : notnull =>
    public static Dictionary<TKey, TEntOut> ToDictionarySafe<TEntIn, TEntOut, TKey>(this IEnumerable<TEntIn> input, Func<TEntIn, TKey> keyExtractor, Func<TEntIn, TEntOut> valueExtractor) where TKey : notnull => input
        .GroupBy(keyExtractor)
        .ToDictionary(
            _ => _.Key,
            _ => valueExtractor(_.First())
            );
    public static Dictionary<TKey, TEnt> ToDictionarySafe<TEnt, TKey>(this IEnumerable<TEnt> input, Func<TEnt, TKey> keyExtractor) where TKey : notnull => input.ToDictionarySafe(keyExtractor, _ => _);



    public static Dictionary<TKey, IReadOnlyCollection<TInput>> GroupAndToDictionary<TInput, TKey>(this IEnumerable<TInput> input, Func<TInput, TKey> grouping) where TKey : notnull =>
        GroupAndToDictionary(input, grouping, _ => _);

    public static Dictionary<TKey, IReadOnlyCollection<TOutput>> GroupAndToDictionary<TInput, TKey, TOutput>(
        this IEnumerable<TInput> input,
        Func<TInput, TKey> grouping,
        Func<TInput, TOutput> outputFunc) where TKey : notnull =>
        input.GroupBy(grouping)
          .ToDictionary(
            _ => _.Key,
            _ => _.Select(outputFunc).ToReadonlyCollection()
            );

    public static (Dictionary<TKey, IReadOnlyCollection<TInput>> First, Dictionary<TKey, IReadOnlyCollection<TInput>> Second) GroupAndToDictionary<TInput, TKey>(
        this (IEnumerable<TInput>, IEnumerable<TInput>) input, Func<TInput, TKey> grouping) where TKey : notnull =>
        (input.Item1.GroupAndToDictionary(grouping), input.Item2.GroupAndToDictionary(grouping));


    public static (Dictionary<TKey, TInput> Fist, Dictionary<TKey, TInput>) ToDictionary<TInput, TKey>(
        this (IEnumerable<TInput>, IEnumerable<TInput>) input,
        Func<TInput, TKey> grouping
        ) where TKey : notnull =>
        (input.Item1.ToDictionary(grouping), input.Item2.ToDictionary(grouping));



    public static Dictionary<T, T> WithOffset<T>(this IEnumerable<T> input, int offSet, bool sortEntries = false) where T : notnull
    {
        var returnee = new Dictionary<T, T>();
        var asList = input.ToList();
        if (sortEntries)
            asList = input.OrderBy(en => en).ToList();
        if (offSet < 0)
            asList = asList.Reverse<T>().ToList();
        var absOffset = Math.Abs(offSet);
        for (int i = 0; i + absOffset < asList.Count; i++)
        {
            returnee[asList[i]] = asList[i + absOffset];
        }
        return returnee;
    }

    public static IReadOnlyCollection<B> Collect<A, B>(this IEnumerable<A?> inputs, Func<A, B?> collector) => inputs
        .Where(_ => _ is not null)
        .Select(_ => collector(_!))
        .Collect();

    public static IReadOnlyCollection<A> Collect<A>(this IEnumerable<A?> inputs) => inputs
        .Where(_ => _ is not null)
        .Select(_ => _!)
        .ToList();

    public static IReadOnlyCollection<B> CollectMany<A, B>(this IEnumerable<A?> inputs, Func<A, IEnumerable<B?>?> collector) => inputs
        .Where(_ => _ is not null)
        .Select(_ => collector(_!))
        .Where(_ => _ is not null)
        .SelectMany(_ => _!)
        .Collect();



    public static (IReadOnlyCollection<A> Succeeded, IReadOnlyCollection<A> Failed) Partition<A>(this IEnumerable<A> input, Func<A, bool> filter) =>
        (input.Where(filter).ToList(), input.Where(inp => !filter(inp)).ToList());

    public static (IReadOnlyCollection<A> SucceededFirst, IReadOnlyCollection<A> SucceededSecond, IReadOnlyCollection<A> Failed) Partition<A>(this IEnumerable<A> input, Func<A, bool> filterFirst, Func<A, bool> filterSecond)
    {
        var (succeededFirst, succeededSecond, failed) = (new List<A>(), new List<A>(), new List<A>());
        foreach (var inp in input)
        {
            if (filterFirst(inp))
                succeededFirst.Add(inp);
            else if (filterSecond(inp))
                succeededSecond.Add(inp);
            else
                failed.Add(inp);
        }
        return (succeededFirst, succeededSecond, failed);
    }

    public static IReadOnlyCollection<(A Input, B Acummulated)> PassThrough<A, B>(this IEnumerable<A> input, B initialState, Func<A, B, (A, B)> accumulator)
    {
        var returnee = new List<(A, B)>();
        var state = initialState;
        foreach (var inp in input)
        {
            var result = accumulator(inp, state);
            state = result.Item2;
            returnee.Add(result);
        }
        return returnee;
    }

    public static IReadOnlyCollection<A> PassOver<A, B>(this IEnumerable<A> input, B initialState, Func<A, B, (A, B)> accumulator) => input
        .PassThrough(initialState, accumulator)
        .Select(_ => _.Input)
        .ToList();

    public static (IReadOnlyCollection<B> Succeded, IReadOnlyCollection<B> Failed) Select<A, B>(this (IEnumerable<A> Succeded, IEnumerable<A> Failed) input, Func<A, B> map) =>
        (input.Succeded.Select(map).ToList(), input.Failed.Select(map).ToList());

    public static (IReadOnlyCollection<B> SuccededFirst, IReadOnlyCollection<B> SuccededSecond, IReadOnlyCollection<B> Failed) Select<A, B>(this (IEnumerable<A> SuccededFirst, IEnumerable<A> SuccededSecond, IEnumerable<A> Failed) input, Func<A, B> map) =>
        (input.SuccededFirst.Select(map).ToList(), input.SuccededSecond.Select(map).ToList(), input.Failed.Select(map).ToList());


    public static IReadOnlyCollection<(TFirst First, TSecond Second)> MatchedWith<TFirst, TSecond, TKey>(this IEnumerable<TFirst> firsts, IEnumerable<TSecond> seconds, Func<TFirst, TKey> byFirst, Func<TSecond, TKey> bySecond)
        where TKey : notnull
    {
        var secondsMap = seconds
            .ToDictionarySafe(bySecond);
        var returnee = firsts
            .Select(_ => (Key: byFirst(_), Entry: _))
            .Where(_ => secondsMap.ContainsKey(_.Key))
            .Select(_ => (_.Entry, secondsMap[_.Key]))
            .ToList();
        return returnee;
    }

    /// <summary>
    /// Given 2 sequences:
    /// <list type="number">
    /// <item>
    /// <term>first:</term>
    /// <description>1,2,3,4</description>
    /// </item>
    /// <item>
    /// <term>second:</term>
    /// <description>a,b</description>
    /// </item>
    /// </list>
    ///  produces sequence: [1,a,2,b,3,4]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>

    public static IReadOnlyCollection<T> Interleave<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        if (!first.Any())
            return second.ToReadonlyCollection();
        if (!second.Any())
            return first.ToReadonlyCollection();
        var now = new Queue<T>(first);
        var next = new Queue<T>(second);
        var returnee = new List<T>();
        while (now.TryDequeue(out var popped)) {
            returnee.Add(popped);
            (next, now) = (now, next);
        }
        if (next.Any())
            returnee.AddRange(next);
        return returnee;
    }



    public static string MakeString<A>(this IEnumerable<A> input, string separator) => string.Join(separator, input);

    public static string MakeString<A>(this IEnumerable<A> input, string prefix, string separator, string suffix)
    {
        var returnee = new StringBuilder();
        returnee.Append(prefix);
        returnee.Append(input.MakeString(separator));
        returnee.Append(suffix);
        return returnee.ToString();
    }


}
