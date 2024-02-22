using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DevTest.Helpers
{
    public class AzureStorageHelper
    {
        public static async Task<CloudBlobContainer> GetContainerForDevTest(string accountName, string accountKey)
        {
            var cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("dev-test-files");
            if (!(await container.ExistsAsync()))
            {
                await container.CreateIfNotExistsAsync();
            }

            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            return container;
        }
    }
}
