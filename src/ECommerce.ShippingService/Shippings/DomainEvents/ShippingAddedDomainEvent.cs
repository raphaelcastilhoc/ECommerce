using MediatR;

namespace ECommerce.ShippingService.Shippings.DomainEvents
{
    public class ShippingAddedDomainEvent : INotification
    {
        public ShippingAddedDomainEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
