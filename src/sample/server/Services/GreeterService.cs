using System.Threading.Tasks;
using Grpc.Core;
using Krondev.Greeter;
using Microsoft.Extensions.Logging;
using Status = Krondev.Greeter.Status;

namespace server
{
    public class GreeterService : 
        Krondev.Greeter.GreeterService.GreeterServiceBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name,
                Status = Status.Success
            });
        }
    }
}
