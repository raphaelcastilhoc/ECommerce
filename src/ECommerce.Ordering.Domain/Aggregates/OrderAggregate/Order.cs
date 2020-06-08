using ECommerce.Ordering.Domain.Events;
using ECommerce.SeedWork;
using System;
using System.Collections.Generic;

namespace ECommerce.Ordering.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity<int>, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems;

        public Order(int buyerId)
        {
            BuyerId = buyerId;
            _orderItems = new List<OrderItem>();
            Date = DateTime.Now;
        }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public int BuyerId { get; private set; }

        public DateTime Date { get; private set; }

        public void AddOrderItem(int productId, string name, int quantity)
        {
            var orderItem = new OrderItem(name, quantity);
            _orderItems.Add(orderItem);

            AddDomainEvent(new OrderAddedDomainEvent(productId, quantity));
        }
    }
}
