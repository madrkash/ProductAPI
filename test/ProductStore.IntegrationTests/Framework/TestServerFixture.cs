using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProductStore.API;
using Serilog;
using System;
using System.IO;
using System.Net.Http;

namespace ProductStore.IntegrationTests.Framework
{
    public class TestServerFixture : IDisposable
    {
        private readonly IHost _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webHost =>
                    webHost
                        .UseStartup<Startup>()
                        .ConfigureAppConfiguration((context, config) => config
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"appsettings.json"))
                        .UseSerilog()
                        .UseTestServer()
                );

            _testServer = hostBuilder.Start();

            Client = _testServer.GetTestClient();
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Client.Dispose();
                    _testServer.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}