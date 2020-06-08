using ECommerce.Ordering.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.DomainEventHandlers
{
    public class OrderAddedDomainEventHandler : INotificationHandler<OrderAddedDomainEvent>
    {
        public async Task Handle(OrderAddedDomainEvent orderAddedDomainEvent, CancellationToken cancellationToken)
        {
            //Publish message to event bus
        }
    }
}
