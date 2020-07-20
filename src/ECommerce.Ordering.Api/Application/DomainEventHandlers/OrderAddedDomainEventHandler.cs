using ECommerce.EventBus;
using ECommerce.Ordering.Api.Application.IntegrationEvents;
using ECommerce.Ordering.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.DomainEventHandlers
{
    public class OrderAddedDomainEventHandler : INotificationHandler<OrderAddedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public OrderAddedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(OrderAddedDomainEvent orderAddedDomainEvent, CancellationToken cancellationToken)
        {
            var orderAddedEvent = new OrderAddedEvent(orderAddedDomainEvent.ProductId, orderAddedDomainEvent.Quantity);
            _eventBus.Publish(orderAddedEvent);

            return Task.CompletedTask;
        }
    }
}
