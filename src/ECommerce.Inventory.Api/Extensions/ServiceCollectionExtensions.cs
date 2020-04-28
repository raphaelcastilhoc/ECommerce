using ECommerce.Inventory.Api.Application.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Inventory.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
        {
            var applicationAssembly = typeof(GetProductByIdQueryHandler).Assembly;
            services.AddMediatR(applicationAssembly);

            return services;
        }
    }
}
