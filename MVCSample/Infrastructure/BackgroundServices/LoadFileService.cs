using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.BackgroundServices
{
    public class LoadFileService : BackgroundService
    {
        private IMemoryCache _cash { get; set; }

        private IExampleRestClient _restClient { get; set; }

        public LoadFileService(IMemoryCache cash, IExampleRestClient restClient)
        {
            _cash = cash;
            _restClient = restClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var image = _restClient.GetFile();
                if(image != null)
                {
                    var cashKey = $"image_{DateTime.UtcNow:yyy_MM_dd}";
                    var entryOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                    };
                    _cash.Set(cashKey, image, entryOptions);
                }
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

    }
}
