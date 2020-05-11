using ECommerce.Location.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using Xunit;

namespace ECommerce.Location.IntegrationTests
{
    public class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public HttpClient HttpClient { get; private set; }

        public BaseIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            var projectDir = Directory.GetCurrentDirectory();
            string configPath = Path.Combine(projectDir, "appsettings.json");

            _factory = factory;
            HttpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });

            }).CreateClient();
        }
    }
}
