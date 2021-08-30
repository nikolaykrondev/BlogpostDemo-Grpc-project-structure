using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Krondev.Greeter.Configuration;

namespace Krondev.Greeter.Clients
{
    public class GreeterClient : 
        GreeterService.GreeterServiceBase,
        IGreeterClient,
        IAsyncDisposable
    {
        private readonly GreeterService.GreeterServiceClient _baseClient;
        
        private readonly string _fullName;
        private readonly GrpcChannel _channel;
        
        public GreeterClient(ChannelCredentials channelCredentials,
            GreeterClientOptions options = default)
        {
            if (channelCredentials == null)
                throw new ArgumentNullException(nameof(channelCredentials));

            options ??= new GreeterClientOptions();
            
            _channel = GrpcChannel.ForAddress(options.Host + ':' + options.Port, new GrpcChannelOptions
            {
                Credentials = channelCredentials,
                HttpHandler = options.HttpHandler
            });
            _baseClient = new GreeterService.GreeterServiceClient(_channel);

            _fullName = GetType().FullName;
        }
        
        public async Task<HelloReply> SayHello(HelloRequest request, CancellationToken cancellationToken = default)
        {
            // do your extra pre-logic...
            try
            {
                using var call = _baseClient.SayHelloAsync(
                    request,
                    CreateTraceMetaData(),
                    cancellationToken: cancellationToken);
                var response = await call.ResponseAsync;
                return response;
            }
            catch (Exception)
            {
                // do your extra logging here 
                throw;
            }
        }

        private static Metadata CreateTraceMetaData()
        {
            var metadata = new Metadata
            {
                {
                    "correlation-id",
                    Guid.NewGuid().ToString()   // replace with your logic
                }
            };

            return metadata;
        }
        
        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
            {
                await _channel.ShutdownAsync();
            }
        }
    }
}