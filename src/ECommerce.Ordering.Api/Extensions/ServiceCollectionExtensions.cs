using ECommerce.Ordering.Api.Application.Commands;
using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using ECommerce.Ordering.Infrastructure;
using ECommerce.Ordering.Infrastructure.Repositories;
using MediatR;
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
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBuyerRepository, BuyerRepository>();

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = "Data Source=DESKTOP-1R3EEK7\\SQLEXPRESS;Initial Catalog=Ordering;User id=sa;Password=Qazplm27; Integrated Security=True";

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
    }
}
