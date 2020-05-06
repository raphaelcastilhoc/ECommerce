﻿using ECommerce.SeedWork;
using FluentValidation;

namespace ECommerce.Inventory.Domain.Aggregates.ProductAggregate
{
    public class Product : Entity<int>, IAggregateRoot
    {
        public Product(string name, string description, int quantity)
        {
            Name = name;
            Description = description;
            Quantity = quantity;

            var validator = new ProductValidator();
            validator.ValidateAndThrow(this);
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int Quantity { get; private set; }
    }
}
