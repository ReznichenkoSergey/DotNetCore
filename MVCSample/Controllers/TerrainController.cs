using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MVCSample.Infrastructure.Services.Interfaces;
using RestSharp;
using MVCSample.Models.ViewModels;
using MVCSample.Infrastructure.Services.Implementations;
using Microsoft.Extensions.Options;
using MVCSample.Infrastructure.Configuration;

namespace MVCSample.Controllers
{
    public class TerrainController : Controller
    {
        private IMemoryCache _cash { get; set; }

        private IExampleRestClient _restClient { get; set; }
        public IFileKeyCreator _creator { get; }
        private IFileProcessing _channel { get; }

        //public TerrainController(IMemoryCache cash, IExampleRestClient restClient, IFileKeyCreator creator, FileProcessingChannel channel)
        public TerrainController(IMemoryCache cash, IExampleRestClient restClient, IFileKeyCreator creator, IFileProcessing channel)
        {
            _cash = cash;
            _restClient = restClient;
            _creator = creator;
            _channel = channel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult LoadDefaultFiles()
        {
            var fileList = _restClient.GetDefaultFiles();
            return View(fileList);
        }

        [AllowAnonymous]
        public FileContentResult Image([FromServices] IOptions<MemmoryCashConfig> config, string fileName)
        {
            //var cashKey = $"image_{DateTime.UtcNow:yyy_MM_dd}";
            var cashKey = _creator.GetKey(fileName);
            var image = _cash.Get<byte[]>(cashKey);
            if (image == null)
            {
                var entryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(config.Value.ExpirationTimeValueInMinutes));
                image = _restClient.GetFile(fileName);
                //_cash.Set<byte[]>(cashKey, image);
                _cash.Set(cashKey, image, entryOptions);
            }
            return new FileContentResult(image, "image/png");
        }

        [HttpPost]
        [AllowAnonymous]
        //public async Task<IActionResult> Upload([FromForm] TerrainUploadViewModel uploadfile)
        public IActionResult Upload([FromForm] TerrainUploadViewModel uploadfile)
        {
            if (uploadfile.File?.Length > 0)
            {
                //_channel.SetAsync(uploadfile.File);
                _channel.Set(uploadfile.File);

                uploadfile.Stage = UploadStage.Completed;
                uploadfile.File = null;
            }

            return View(uploadfile);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
    }
}
