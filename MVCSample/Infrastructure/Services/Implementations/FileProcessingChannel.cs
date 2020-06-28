using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class FileProcessingChannel
    {
        private Channel<IFormFile> _channel;

        public FileProcessingChannel()
        {
            _channel = Channel.CreateUnbounded<IFormFile>();
        }

        /*public async Task SetAsync(IFormFile file)
        {
            await _channel.Writer.WriteAsync(file);
        }*/

        public void Set(IFormFile file)
        {
            _channel.Writer.TryWrite(file);
        }

        /*public IAsyncEnumerable<IFormFile> GetAllAsync()
        {
            return _channel.Reader.ReadAllAsync();
        }*/

        public List<IFormFile> GetAll()
        {
            List<IFormFile> list = new List<IFormFile>();
            if(_channel.Reader.TryRead(out IFormFile item))
                list.Add(item);
            return list;
        }

    }
}
