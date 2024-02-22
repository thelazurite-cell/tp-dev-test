using Microsoft.WindowsAzure.Storage.Blob;

namespace DevTest.Interfaces
{
    public interface IBlobContainerService
    {
        Task<bool> FileNameExists(string fileName, CloudBlobContainer? container);

        Task<KeyValuePair<string,string>?> UploadFile(string fileName, Stream fileBytes);
    }
}
