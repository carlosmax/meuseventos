using MeusEventos.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.IO;
using System.Net.Http;

namespace MeusEventos.Tests.Integration
{
    public class Environment
    {
        public static TestServer Server { get; set; }
        public static HttpClient Client { get; set; }

        public static void CreateServer()
        {
            Server = new TestServer(
                new WebHostBuilder()
                    .UseKestrel()
                    .UseEnvironment("Testing")
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseUrls("http://localhost:8285")
                    .UseStartup<Startup>());

            Client = Server.CreateClient();
        }
    }
}
