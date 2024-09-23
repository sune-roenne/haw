namespace ThousandAcreWoods.Language.Streamables;

public interface IStreamable
{
    public static IStreamable<T> Combine<T>(params IStreamable<T>[] containers) => new CombinedStreamContainer<T>(containers);

}

public interface IStreamable<T>
{
    IAsyncEnumerable<IReadOnlyCollection<T>> Consume(CancellationToken cancellationToken);
    IStreamable<T> CombineWith(IStreamable<T> other) => new CombinedStreamContainer<T>(this, other);



}
