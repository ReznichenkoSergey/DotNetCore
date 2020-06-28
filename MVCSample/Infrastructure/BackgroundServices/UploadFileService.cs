using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCSample.Infrastructure.Services.Implementations;
using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.BackgroundServices
{
    public class UploadFileService : BackgroundService
    {
        /*private FileProcessingChannel _channel { get; set; }

        private IExampleRestClient _restClient { get; set; }

        public UploadFileService(FileProcessingChannel channel, IExampleRestClient restClient)
        {
            _channel = channel;
            _restClient = restClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var file in _channel.GetAllAsync())
            {
                _restClient.UploadFile(file);
            }
        }*/

        IServiceProvider _services;
        public UploadFileService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using(var scope = _services.CreateScope())
            {
                await scope
                    .ServiceProvider
                    .GetRequiredService<IScopeService<FileUpload>>()
                    .DoWorkAsync(stoppingToken);
            }
        }
    }
}
