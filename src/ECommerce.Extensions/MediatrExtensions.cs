using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Extensions
{
    public static class MediatrExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, IEnumerable<INotification> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
