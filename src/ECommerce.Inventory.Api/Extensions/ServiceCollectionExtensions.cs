using ECommerce.Inventory.Domain.Aggregates.ProductAggregate;
using ECommerce.Inventory.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Inventory.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
