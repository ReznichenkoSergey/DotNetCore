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
        public FileContentResult GetFile()
        {
            var fileBytes = System.IO.File.ReadAllBytes("wwwroot/About.png");
            return new FileContentResult(fileBytes, "image/png");
        }

        [HttpPost("File")]
        public void UploadFile([FromBody] string file, [FromQuery] string fileName, [FromServices] IWebHostEnvironment webHost)
        {
            var fileBytes = Convert.FromBase64String(file);
            var filePath = Path.Combine(webHost.WebRootPath, fileName);
            System.IO.File.WriteAllBytes(filePath, fileBytes);
        }
    }
}
