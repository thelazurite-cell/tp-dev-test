using DevTest.Interfaces;
using DevTest.Models;
using Microsoft.Extensions.Options;

namespace DevTest.Services
{
    public class ImageUploadService(IOptions<AppSettings> appSettings, IBlobContainerService blobContainerService) : GenericFileUploadService(appSettings)
    {
        private readonly AppSettings appSettings = appSettings.Value;

        public override bool MatchesFileTypeConstraint(string fileName)
        {
            return appSettings.AllowedImageFileTypes.Any(itm => fileName.ToLower().EndsWith(itm.ToLower()));
        }

        public override async Task<KeyValuePair<string, string>?> UploadFile(IFormFile postedFile)
        {
            var fileName = postedFile.FileName;
            using var stream = new MemoryStream(new byte[postedFile.Length]);
            await postedFile.CopyToAsync(stream);
            stream.Position = 0;

            return await blobContainerService.UploadFile(postedFile.FileName, stream);
        }
    }
}
