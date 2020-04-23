using ECommerce.SeedWork;

namespace ECommerce.Inventory.Domain.Aggregates.ProductAggregate
{
    public class Product : Entity<int>, IAggregateRoot
    {
        public Product(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; private set; }

        public int Quantity { get; private set; }
    }
}
