using ECommerce.SeedWork;

namespace ECommerce.Ordering.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity<int>
    {
        public OrderItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; private set; }

        public int Quantity { get; private set; }
    }
}
