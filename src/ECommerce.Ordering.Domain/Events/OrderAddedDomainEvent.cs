using MediatR;

namespace ECommerce.Ordering.Domain.Events
{
    public class OrderAddedDomainEvent : INotification
    {
        public OrderAddedDomainEvent(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public int ProductId { get; }

        public int Quantity { get; }
    }
}
