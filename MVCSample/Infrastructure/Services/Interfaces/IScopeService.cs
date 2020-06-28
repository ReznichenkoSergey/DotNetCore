using System.Threading;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Interfaces
{
    public interface IScopeService<T> 
        where T: IMyScope
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }

    public class FileLoad : IMyScope { }

    public class FileUpload : IMyScope { }

    public interface IMyScope { }
}
