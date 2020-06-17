using MassTransit;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Api.Application.IntegrationEvents
{
    public class OrderAddedDomainEventConsumer : IConsumer<OrderAddedDomainEvent>
    {
        public Task Consume(ConsumeContext<OrderAddedDomainEvent> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
