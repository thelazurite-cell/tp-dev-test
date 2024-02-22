namespace DevTest.Models
{

    public class FileUploadResponse
    {
        public FileUploadErrorCodeEnum Code { get; set; } = FileUploadErrorCodeEnum.None;
        public string Message { get; set; } = string.Empty;
        public FileUploadData? Data { get; set; }
        public List<string>? Error { get; set; }
    }

    public class FileUploadData
    {
        public string FileName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
