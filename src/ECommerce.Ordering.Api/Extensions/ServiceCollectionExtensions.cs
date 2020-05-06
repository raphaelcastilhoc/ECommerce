using ECommerce.ExternalHandlers.Http;
using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using ECommerce.Ordering.Infrastructure;
using ECommerce.Ordering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ECommerce.Ordering.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();

            services.AddScoped<IHttpHandler, HttpHandler>();

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
    }
}
