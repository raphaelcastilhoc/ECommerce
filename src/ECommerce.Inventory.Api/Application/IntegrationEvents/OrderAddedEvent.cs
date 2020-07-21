using ECommerce.EventBus;

namespace ECommerce.Inventory.Api.Application.IntegrationEvents
{
    public class OrderAddedEvent : IntegrationEvent
    {
        public int ProductId { get; }

        public int Quantity { get; }
    }
}
