using MVCSample.Infrastructure.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class ScopeUploadService : IScopeService<FileUpload>
    {
        private FileProcessingChannel _channel { get; set; }

        private IExampleRestClient _restClient { get; set; }

        public ScopeUploadService(FileProcessingChannel channel, IExampleRestClient restClient)
        {
            _channel = channel;
            _restClient = restClient;
        }
        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            /*await foreach (var file in _channel.GetAllAsync())
            {
                _restClient.UploadFile(file);
            }*/
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    foreach (var file in _channel.GetAll())
                    {
                        _restClient.UploadFile(file);
                    }
                });
            }
        }

    }
}
