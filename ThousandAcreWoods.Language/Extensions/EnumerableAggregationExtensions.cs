namespace ThousandAcreWoods.Language.Extensions;

public static class EnumerableAggregationExtensions
{

    public static TOut Fold<TIn, TOut>(this IEnumerable<TIn> inputs, TOut initial, Func<TOut, TIn, TOut> aggregator)
    {
        var returnee = initial;
        foreach (var input in inputs)
            returnee = aggregator(returnee, input);
        return returnee;
    }

}
