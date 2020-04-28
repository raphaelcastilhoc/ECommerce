using ECommerce.Ordering.Api.Application.Commands;
using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using ECommerce.Ordering.Infrastructure;
using ECommerce.Ordering.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;

namespace ECommerce.Ordering.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionString");

            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<OrderingContext>(options =>
                   {
                       options.UseSqlServer(connectionString,
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped
                   );

            services.AddScoped<IDbConnection>(x => new SqlConnection(connectionString));

            return services;
        }

        public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
        {
            var applicationAssembly = typeof(AddBuyerCommandHandler).Assembly;
            services.AddMediatR(applicationAssembly);

            return services;
        }

        public static IServiceCollection AddCustomHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient("Inventory", client =>
                {
                    client.BaseAddress = new Uri(configuration.GetValue<string>("ExternalInventoryBaseUrl"));
                })
                .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                retryCount: 5,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    //Log.Information($"Delaying for { timespan.Seconds } seconds, then making retry { retryAttempt }. Error: {outcome?.Result?.ToString()}");
                });
        }
    }
}
