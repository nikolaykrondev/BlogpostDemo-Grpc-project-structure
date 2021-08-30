using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Grpc.Core;
using Krondev.Greeter;
using Krondev.Greeter.Clients;
using Krondev.Greeter.Configuration;

namespace client
{
    class Program
    {
        private static async Task Main()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                // The following statement allows you to call insecure services. To be used only in development environments.
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }

            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            await using var client = new GreeterClient(ChannelCredentials.Insecure, new GreeterClientOptions
            {
                Host = "http://localhost",
                Port = 5000,
                HttpHandler = httpHandler
            });

            var reply = await client.SayHello(
                new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message + " with status " + reply.Status);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
