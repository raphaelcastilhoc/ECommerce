using ECommerce.SeedWork;

namespace ECommerce.Ordering.Domain.Aggregates.BuyerAggregate
{
    public class Buyer : Entity<int>, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
