using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Interfaces
{
    public interface IExampleRestClient
    {
        byte[] GetFile(string fileName);

        List<string> GetDefaultFiles();

        void UploadFile(IFormFile file);
    }
}
