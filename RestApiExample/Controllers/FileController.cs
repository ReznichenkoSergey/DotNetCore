using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
