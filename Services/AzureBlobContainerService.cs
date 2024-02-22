using DevTest.Helpers;
using DevTest.Interfaces;
using DevTest.Models;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Reflection.Metadata;
using static System.Reflection.Metadata.BlobBuilder;

namespace DevTest.Services
{
    public class AzureBlobContainerService(IOptions<AppSettings> configuration) : IBlobContainerService
    {
        private readonly AppSettings appSettings = configuration.Value;
        //private readonly StorageCredentials storageCredentials;
        //private CloudStorageAccount storageAccount;

        //public AzureBlobContainerService(IConfiguration configuration)
        //{

            //this.storageCredentials = new StorageCredentials(this.appSettings.CloudAccountName, this.appSettings.CloudAccountKey); ;
            //this.storageAccount = new CloudStorageAccount(storageCredentials, true);
        //}

        public async Task<bool> FileNameExists(string fileName, CloudBlobContainer? container)
        {
            container ??= await AzureStorageHelper.GetContainerForDevTest(appSettings.CloudAccountName, appSettings.CloudAccountKey);
            var blob = container.GetBlockBlobReference(fileName);
            return await blob.ExistsAsync();
        }

        /// <summary>
        /// Upload the file to the blob storage. If a file with the same name has been created
        /// Assign a uniquely generated prefix before uploading.
        /// </summary>
        /// <param name="fileName">The file name to be used as the blob reference</param>
        /// <param name="fileStream">The file's stream</param>
        /// <returns>
        ///     the file  will be returned if successful, otherwise empty.
        ///     in a real world scenario, I would've implemented a logger here
        ///     so that this could be monitored via Kibana or another tool for logging.
        /// </returns>
        public async Task<KeyValuePair<string,string>?> UploadFile(string fileName, Stream fileStream)
        {
            try
            {
                var container = await AzureStorageHelper.GetContainerForDevTest(appSettings.CloudAccountName, appSettings.CloudAccountKey);
                if (await FileNameExists(fileName, container))
                {
                    fileName = $"{Guid.NewGuid()}-{fileName}";
                }
                var blobReference = container.GetBlockBlobReference(fileName);
                await blobReference.UploadFromStreamAsync(fileStream);

                return new(fileName, blobReference.Uri.AbsoluteUri);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //private async Task<CloudBlobContainer> GetContainer()
        //{
        //    var client = storageAccount.CreateCloudBlobClient();
        //    var container = client.GetContainerReference(appSettings.CloudContainerName);
        //    await container.CreateIfNotExistsAsync();
        //    return container;
        //}
    }
}
