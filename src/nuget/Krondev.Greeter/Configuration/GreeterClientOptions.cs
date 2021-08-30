using System.Net.Http;

namespace Krondev.Greeter.Configuration
{
    public class GreeterClientOptions
    {
        public string Host { get; set; } = "greeter-service";
        public int Port { get; set; } = 5000;
        public HttpClientHandler HttpHandler { get; set; }
    }
}