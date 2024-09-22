namespace ThousandAcreWoods.Book.Hosting.Wasm.Persistence;

public interface IFileDownloadInitiator
{
    Task StartDownload(string resourceUri, string saveFileName);


}
