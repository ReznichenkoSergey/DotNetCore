using Microsoft.AspNetCore.Http;
using MVCSample.Infrastructure.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class ExampleRestClient : IExampleRestClient
    {
        public byte[] GetFile()
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("File", Method.GET);
            var result = client.Execute(request).RawBytes;
            return result;
        }

        public void UploadFile(IFormFile file)
        {
            var client = new RestClient("http://localhost:5000");
            var request = new RestRequest("File", Method.POST);
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var imageBytes = ms.ToArray();
                    request.AddJsonBody(Convert.ToBase64String(imageBytes));
                }

                request.AddQueryParameter("fileName", file.FileName);
                client.Execute(request);
            }
        }
    }
}
