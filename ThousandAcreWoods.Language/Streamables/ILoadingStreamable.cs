namespace ThousandAcreWoods.Language.Streamables;
public interface ILoadingStreamable<T> : IStreamable<T>
{
    Task ReloadAndPublish();
}
