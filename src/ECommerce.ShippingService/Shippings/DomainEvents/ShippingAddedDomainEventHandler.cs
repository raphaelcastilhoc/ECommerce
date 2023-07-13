using ECommerce.EventBus;
using ECommerce.ShippingService.Shippings.IntegrationEvents;
using MediatR;

namespace ECommerce.ShippingService.Shippings.DomainEvents
{
    public class ShippingAddedDomainEventHandler : INotificationHandler<ShippingAddedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public ShippingAddedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(ShippingAddedDomainEvent shippingAddedDomainEvent, CancellationToken cancellationToken)
        {
            var shippingAddedEvent = new ShippingAddedEvent(shippingAddedDomainEvent.Id);
            _eventBus.Publish(shippingAddedEvent);

            return Task.CompletedTask;
        }
    }
}
