namespace DevTest.Models
{
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the value for the cloud account name.
        /// Ideally if possible we should be storing these as an environment variable
        /// or as a secret, instead of in plain text in appsettings.
        /// </summary>
        public string CloudAccountName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value for the API Key to access the azure storage.
        /// </summary>
        public string CloudAccountKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the cloud container name to access.
        /// </summary>
        public string CloudContainerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the global limit for file size uploads.
        /// </summary>
        public int MaxFileSizeInMb { get; set; }

        /// <summary>
        /// Gets or sets the list of allowed file types for an image upload operation.
        /// </summary>
        public List<string> AllowedImageFileTypes { get; set; } = new();
    }
}
