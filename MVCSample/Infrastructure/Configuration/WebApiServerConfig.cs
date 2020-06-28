namespace MVCSample.Infrastructure.Configuration
{
    public class WebApiServerConfig
    {
        public string Address { get; set; }

        public string GetDefaultFilesMethodName { get; set; }

        public string GetFileByFilenameMethodName { get; set; }

        public string UploadFileMethodName { get; set; }
    }
}
