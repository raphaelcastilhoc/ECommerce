using ECommerce.Inventory.Api.Application.IntegrationEvents;
using ECommerce.Inventory.Domain.Aggregates.ProductAggregate;
using ECommerce.Inventory.Infrastructure.Repositories;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace ECommerce.Inventory.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderAddedDomainEventConsumer>();

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    // configure health checks for this bus instance
                    //cfg.UseHealthCheck(context);

                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("OrderAddedDomainEvent", ep =>
                    {
                        ep.PrefetchCount = 0;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ExchangeType = ExchangeType.Direct;
                        ep.Durable = true;
                        ep.Bind("Ordering");

                        ep.ConfigureConsumer<OrderAddedDomainEventConsumer>(context);
                    });
                }));
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
