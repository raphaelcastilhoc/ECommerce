using ECommerce.EventBus;

namespace ECommerce.ShippingService.Shippings.IntegrationEvents
{
    public class ShippingAddedEvent : IntegrationEvent
    {
        public ShippingAddedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
