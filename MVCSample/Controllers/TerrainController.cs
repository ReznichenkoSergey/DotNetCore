using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MVCSample.Infrastructure.Services.Interfaces;
using RestSharp;
using System.Threading.Channels;
using MVCSample.Models.ViewModels;
using MVCSample.Infrastructure.Services.Implementations;

namespace MVCSample.Controllers
{
    public class TerrainController : Controller
    {
        private IMemoryCache _cash { get; set; }

        private IExampleRestClient _restClient { get; set; }

        private FileProcessingChannel _channel { get; }

        public TerrainController(IMemoryCache cash, IExampleRestClient restClient, FileProcessingChannel channel)
        {
            _cash = cash;
            _restClient = restClient;
            _channel = channel;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public FileContentResult Image()
        {
            var cashKey = $"image_{DateTime.UtcNow:yyy_MM_dd}";
            var image = _cash.Get<byte[]>(cashKey);
            if (image == null)
            {
                var entryOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                };

                image = _restClient.GetFile();
                //_cash.Set<byte[]>(cashKey, image);
                _cash.Set(cashKey, image, entryOptions);
            }
            return new FileContentResult(image, "image/png");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Upload([FromForm] TerrainUploadViewModel uploadfile)
        {
            if (uploadfile.File?.Length > 0)
            {
                await _channel.SetAsync(uploadfile.File);

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
