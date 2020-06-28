using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MVCSample.Infrastructure.Configuration;
using MVCSample.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class ExampleRestClient : IExampleRestClient
    {
        IOptions<WebApiServerConfig> _config;
        public ExampleRestClient(IOptions<WebApiServerConfig> config)
        {
            _config = config;
        }

        public List<string> GetDefaultFiles()
        {
            var client = new RestClient(_config.Value.Address);
            var request = new RestRequest(_config.Value.GetDefaultFilesMethodName, Method.GET);
            return JsonConvert.DeserializeObject<List<string>>(client.Execute(request).Content);
        }

        public byte[] GetFile(string filename)
        {
            var client = new RestClient(_config.Value.Address);
            var request = new RestRequest(_config.Value.GetFileByFilenameMethodName, Method.GET);
            request.AddQueryParameter("filename", filename);
            return client.Execute(request).RawBytes;
        }

        public void UploadFile(IFormFile file)
        {
            var client = new RestClient(_config.Value.Address);
            var request = new RestRequest(_config.Value.UploadFileMethodName, Method.POST);
            if (file != null)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var imageBytes = ms.ToArray();
                    //request.AddJsonBody(Convert.ToBase64String(imageBytes));
                    request.AddFile("file", imageBytes, file.FileName, "image/png");
                }
                //request.AddQueryParameter("fileName", file.FileName);
                client.Execute(request);
            }
        }
    }
}
