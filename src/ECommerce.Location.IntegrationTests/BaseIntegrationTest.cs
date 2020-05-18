using ECommerce.ExternalHandlers.Http;
using ECommerce.Location.Api;
using ECommerce.Location.IntegrationTests.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Location.IntegrationTests
{
    public class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        private static WebApplicationFactory<Startup> _factory;

        private static DatabaseContext _databaseContext;

        public BaseIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            var appsettingsPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            _factory = factory;
            factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(appsettingsPath);
                })
                .ConfigureServices(services =>
                {
                    //It's just to show how override services
                    services.AddScoped<IHttpHandler, HttpHandlerMock>();
                });

            }).CreateClient();

            ConfigureDatabase();
        }

        public static void Initialize()
        {
            var appSettingsPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(appSettingsPath);
                }).ConfigureServices(services =>
                {
                    //It's just to show how override services
                    services.AddScoped<IHttpHandler, HttpHandlerMock>();
                });

            });

            ConfigureDatabase();
        }

        private static void ConfigureDatabase()
        {
            var configuration = _factory.Factories.First().Server.Host.Services.GetRequiredService<IConfiguration>();
            var connection = configuration.GetConnectionString("SqlServer");

            configuration["ConnectionStrings:SqlServer"] = connection.Replace("%CONTENTROOTPATH%", Environment.CurrentDirectory);

            _databaseContext = new DatabaseContext(configuration["ConnectionStrings:SqlServer"]);
            _databaseContext.CreateDatabase();
        }

        public HttpClient GetHttpClient()
        {
            return _factory.CreateClient();
        }

        public T GetService<T>() where T : class
        {
            return _factory.Factories.First().Server.Host.Services.GetRequiredService<T>();
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            return await _databaseContext.QueryAsync<T>(sql);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql)
        {
            return await _databaseContext.QueryFirstOrDefaultAsync<T>(sql);
        }

        public void CleanData()
        {
            _databaseContext.CleanData();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }
}
