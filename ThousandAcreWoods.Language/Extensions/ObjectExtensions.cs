namespace ThousandAcreWoods.Language.Extensions;
public static class ObjectExtensions
{

    public static B Pipe<A, B>(this A input, Func<A, B> transform) => transform(input);
    public static B Pipe<A, B>(this A? input, A ifNull, Func<A, B> transform) where A : struct
        => (input ?? ifNull).Pipe(transform);
    public static B? PipeOpt<A, B>(this A? input, Func<A, B> transform) where A : struct where B : struct => input is null ?
        default :
        transform(input.Value);

    public static B? PipeOpt<A, B>(this A? input, Func<A, B> transform) where A : class => input is null ?
        default :
        transform(input);



}
