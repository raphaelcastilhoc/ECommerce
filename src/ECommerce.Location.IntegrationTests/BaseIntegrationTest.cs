using ECommerce.ExternalHandlers.Http;
using ECommerce.Location.IntegrationTests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;

namespace ECommerce.Location.IntegrationTests
{
    public class BaseIntegrationTest<T> : IDisposable where T : class
    {
        private readonly TestServer _testServer;

        private DatabaseContext _databaseContext;

        public DatabaseAccess DatabaseAccess { get; private set; }

        public HttpClient Client { get; private set; }

        public BaseIntegrationTest()
        {
            var appSettingsPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

            var builder = new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile(appSettingsPath)
                    .Build())
                .ConfigureTestServices(services =>
                {
                    services.AddScoped<IHttpHandler, HttpHandlerMock>();
                })
                .UseStartup<T>();

            _testServer = new TestServer(builder);

            Client = _testServer.CreateClient();

            ConfigureDatabase();
        }

        private void ConfigureDatabase()
        {
            var configuration = _testServer.Host.Services.GetRequiredService<IConfiguration>();
            var connection = configuration.GetConnectionString("SqlServer");

            configuration["ConnectionStrings:SqlServer"] = connection.Replace("%CONTENTROOTPATH%", Environment.CurrentDirectory);

            _databaseContext = new DatabaseContext(configuration["ConnectionStrings:SqlServer"]);
            _databaseContext.CreateDatabase();

            DatabaseAccess = new DatabaseAccess(_databaseContext);
        }

        public T GetService<T>() where T : class
        {
            return _testServer.Host.Services.GetRequiredService<T>();
        }

        public void CleanData()
        {
            _databaseContext.CleanData();
        }

        public void Dispose()
        {
            Client?.Dispose();
            _testServer?.Dispose();
        }
    }
}
