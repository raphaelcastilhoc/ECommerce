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
            PurchaseDate = DateTime.Now;
        }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public int BuyerId { get; private set; }

        public DateTime PurchaseDate { get; private set; }

        public void AddOrderItem(string name, int quantity)
        {
            var orderItem = new OrderItem(name, quantity);
            _orderItems.Add(orderItem);
        }
    }
}
