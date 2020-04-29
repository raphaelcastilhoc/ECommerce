using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Ordering.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.ToTable("OrderItem");

            orderConfiguration.HasKey(x => x.Id);

            orderConfiguration.Property<string>("Name").IsRequired();
            orderConfiguration.Property<int>("Quantity").IsRequired();
            orderConfiguration.Property<int>("OrderId").IsRequired();
        }
    }
}
