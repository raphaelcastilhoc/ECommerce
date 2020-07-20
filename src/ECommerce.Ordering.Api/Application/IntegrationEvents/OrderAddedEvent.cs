using ECommerce.EventBus;

namespace ECommerce.Ordering.Api.Application.IntegrationEvents
{
    public class OrderAddedEvent : IntegrationEvent
    {
        public OrderAddedEvent(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public int ProductId { get; }

        public int Quantity { get; }
    }
}
