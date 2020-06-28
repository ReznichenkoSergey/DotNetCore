using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace RestApiExample.Controllers
{
    public class FileController : ControllerBase
    {
        [HttpGet("File")]
        public FileContentResult GetFile([FromServices] IWebHostEnvironment webHost, [FromQuery] string fileName)
        {
            var fileBytes = System.IO.File.ReadAllBytes($"{webHost.WebRootPath}/{fileName}");
            return new FileContentResult(fileBytes, "image/png");
        }

        [HttpGet("DefaultFiles")]
        public List<string> GetDefaultFiles([FromServices] IWebHostEnvironment webHost)
        {
            DirectoryInfo dir = new DirectoryInfo(webHost.WebRootPath);
            return dir
                .GetFiles("*.png", SearchOption.TopDirectoryOnly)
                .Select(x => x.Name)
                .ToList();
        }

        /*
        [HttpPost("File")]
        public void UploadFile([FromBody] string file, [FromQuery] string fileName, [FromServices] IWebHostEnvironment webHost)
        {
            var fileBytes = Convert.FromBase64String(file);
            var filePath = Path.Combine(webHost.WebRootPath, fileName);
            System.IO.File.WriteAllBytes(filePath, fileBytes);
        }*/

        [HttpPost("File")]
        public void UploadFile([FromServices] IWebHostEnvironment webHost)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            var filePath = Path.Combine(webHost.WebRootPath, file.FileName);
                            System.IO.File.WriteAllBytes(filePath, fileBytes);
                        }
                    }
                }
            }
        }
    }
}
