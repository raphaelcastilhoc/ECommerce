using ECommerce.EventBus;
using ECommerce.Inventory.Api.Application.IntegrationEvents;
using ECommerce.Inventory.Domain.Aggregates.ProductAggregate;
using ECommerce.Inventory.Infrastructure.Factories;
using ECommerce.Inventory.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ECommerce.Inventory.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IIntegrationEventHandler<OrderAddedEvent>, OrderAddedEventHandler>();

            services.AddSingleton<IMongoDatabase>(x =>
            {
                return new MongoDBFactory(configuration).GetDatabase();
            });

            return services;
        }
    }
}
