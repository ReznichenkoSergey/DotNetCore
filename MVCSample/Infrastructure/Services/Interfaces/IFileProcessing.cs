using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Interfaces
{
    public interface IFileProcessing
    {
        void Set(IFormFile file);
        List<IFormFile> GetAll();
    }
}
