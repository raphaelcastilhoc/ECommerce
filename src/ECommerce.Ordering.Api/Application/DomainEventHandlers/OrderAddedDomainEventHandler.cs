using ECommerce.Ordering.Domain.Events;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.DomainEventHandlers
{
    public class OrderAddedDomainEventHandler : INotificationHandler<OrderAddedDomainEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderAddedDomainEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(OrderAddedDomainEvent orderAddedDomainEvent, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(orderAddedDomainEvent);
        }
    }
}
