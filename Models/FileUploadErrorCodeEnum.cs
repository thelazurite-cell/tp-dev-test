namespace DevTest.Models
{
    public enum FileUploadErrorCodeEnum
    {
        None = 0,
        FileTooLarge = 4001,
        FileTypeConstraintMismatch = 4002,
        ServerError = 5000
    }
}
