using System.Threading.Tasks;

namespace ECommerce.EventBus
{
    public interface IIntegrationEventHandler<TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}
