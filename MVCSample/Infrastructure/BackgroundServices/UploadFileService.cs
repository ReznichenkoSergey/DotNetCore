using Microsoft.Extensions.Hosting;
using MVCSample.Infrastructure.Services.Implementations;
using MVCSample.Infrastructure.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.BackgroundServices
{
    public class UploadFileService:BackgroundService
    {
        private FileProcessingChannel _channel { get; set; }

        private IExampleRestClient _restClient { get; set; }

        public UploadFileService(FileProcessingChannel channel, IExampleRestClient restClient)
        {
            _channel = channel;
            _restClient = restClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var file in _channel.GetAllAsync())
            {
                _restClient.UploadFile(file);
            }
        }
    }
}
