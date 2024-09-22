namespace ThousandAcreWoods.Language;

public static class CollectionExtensions
{
    public static string MakeString<T>(this IEnumerable<T> inp, string separator, string? startWith = null, string? endWith = null) => 
       $"{(startWith ?? "")}{string.Join(separator, inp.Select(_ => _.ToString()))}{(endWith ?? "")}";
}
