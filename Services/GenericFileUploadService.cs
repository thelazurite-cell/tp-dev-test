using DevTest.Interfaces;
using DevTest.Models;
using Microsoft.Extensions.Options;

namespace DevTest.Services
{
    public abstract class GenericFileUploadService(IOptions<AppSettings> appSettings) : IFileUploadService
    {
        private readonly AppSettings appSettings = appSettings.Value;

        public abstract bool MatchesFileTypeConstraint(string fileName);

        public bool MatchesFileSizeConstraint(long fileByteSize)
        {
            return (fileByteSize / Math.Pow(1024, 2)) <= appSettings.MaxFileSizeInMb;
        }

        public abstract Task<KeyValuePair<string,string>?> UploadFile(IFormFile postedFile);
    }
}
