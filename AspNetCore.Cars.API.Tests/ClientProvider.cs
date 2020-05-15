using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace AspNetCore.Cars.API.Tests
{
    public class ClientProvider : IDisposable
    {
        private TestServer Server;
        public HttpClient Client { get; private set; }
        public ClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            Client = server.CreateClient();
        }

        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
