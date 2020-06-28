using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MVCSample.Infrastructure.Configuration;
using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class ScopeLoadService : IScopeService<FileLoad>
    {
        private IMemoryCache _cash;
        private IExampleRestClient _restClient;
        private IOptions<MemmoryCashConfig> _option;
        private IFileKeyCreator _creator;

        public ScopeLoadService(IMemoryCache cash, IExampleRestClient restClient, IOptions<MemmoryCashConfig> option, IFileKeyCreator creator)
        {
            _cash = cash;
            _restClient = restClient;
            _option = option;
            _creator = creator;
        }
        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //var image = _restClient.GetFile(fileName);
                var files = _restClient.GetDefaultFiles();
                if (files != null)
                {
                    foreach (string fileName in files)
                    {
                        var image = _restClient.GetFile(fileName);
                        if (image != null)
                        {
                            //var cashKey = $"image_{DateTime.UtcNow:yyy_MM_dd}";
                            var cashKey = _creator.GetKey(fileName);
                            var entryOptions = new MemoryCacheEntryOptions()
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_option.Value.ExpirationTimeValueInMinutes)
                            };
                            _cash.Set(cashKey, image, entryOptions);
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(_option.Value.ScanningPeriodInMinutes));
            }
        }

    }
}
