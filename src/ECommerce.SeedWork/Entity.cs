using MediatR;
using System.Collections.Generic;

namespace ECommerce.SeedWork
{
    public abstract class Entity<T> where T : struct
    {
        private List<INotification> _domainEvents;

        public T Id { get; protected set; }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
