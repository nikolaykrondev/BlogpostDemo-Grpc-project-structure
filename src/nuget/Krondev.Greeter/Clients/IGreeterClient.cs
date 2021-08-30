using System.Threading;
using System.Threading.Tasks;

namespace Krondev.Greeter.Clients
{
    public interface IGreeterClient
    {
        Task<HelloReply> SayHello(HelloRequest request, CancellationToken cancellationToken = default);
    }
}