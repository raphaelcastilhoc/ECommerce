using ECommerce.EventBus;

namespace ECommerce.Inventory.Api.Application.IntegrationEvents
{
    public class OrderAddedEvent : IntegrationEvent
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
