using ECommerce.EventBus;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Api.Application.IntegrationEvents
{
    public class OrderAddedEventHandler : IIntegrationEventHandler<OrderAddedEvent>
    {
        public Task Handle(OrderAddedEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
