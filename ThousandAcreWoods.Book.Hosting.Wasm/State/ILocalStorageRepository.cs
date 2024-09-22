namespace ThousandAcreWoods.Book.Hosting.Wasm.State;

public interface ILocalStorageRepository
{
    Task<TOut?> Get<TOut>(string key) where TOut : class; 
    Task Set<TIn>(string key, TIn value) where TIn : class;
    Task Clear();
    Task Delete(string key);


}
