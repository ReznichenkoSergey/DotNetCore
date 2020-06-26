using Microsoft.AspNetCore.Http;

namespace MVCSample.Models.ViewModels
{
    public class TerrainUploadViewModel
    {
        public IFormFile File { get; set; }

        public UploadStage Stage { get; set; }
    }

    public enum UploadStage
    {
        Upload,
        Completed
    }
}
