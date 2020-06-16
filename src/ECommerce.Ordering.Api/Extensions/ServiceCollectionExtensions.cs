using ECommerce.ExternalHandlers.Http;
using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using ECommerce.Ordering.Infrastructure;
using ECommerce.Ordering.Infrastructure.Repositories;
using GreenPipes;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConsumer>();

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    // configure health checks for this bus instance
                    //cfg.UseHealthCheck(context);

                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("submit-order", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));

                        ep.ConfigureConsumer<OrderConsumer>(context);
                    });
                }));
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }

    public class OrderConsumer : IConsumer<OrderConsumerEvent>
    {
        public Task Consume(ConsumeContext<OrderConsumerEvent> context)
        {
            return null;
        }
    }

    public class OrderConsumerEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
