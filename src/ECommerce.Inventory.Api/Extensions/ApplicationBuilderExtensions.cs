using ECommerce.EventBus;
using ECommerce.Inventory.Api.Application.IntegrationEvents;
using Microsoft.AspNetCore.Builder;

namespace ECommerce.Inventory.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseEventBus(this IApplicationBuilder app, IEventBus eventBus)
        {
            eventBus.Subscribe<OrderAddedEvent, OrderAddedEventHandler>();
        }
    }
}
