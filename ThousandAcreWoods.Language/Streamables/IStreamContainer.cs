namespace ThousandAcreWoods.Language.Streamables;
public interface IStreamContainer<T> : IStreamable<T>
{
    void SetSingle(T value);
    void Set(IEnumerable<T> values);

}
