namespace DevTest.Interfaces
{
    public interface IFileUploadService
    {
        Task<KeyValuePair<string,string>?> UploadFile(IFormFile postedFile);

        bool MatchesFileTypeConstraint(string fileName);
        bool MatchesFileSizeConstraint(long fileByteSize);
    }
}
