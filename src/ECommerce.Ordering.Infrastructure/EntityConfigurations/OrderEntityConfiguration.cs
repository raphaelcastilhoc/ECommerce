﻿using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ECommerce.Ordering.Infrastructure.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.ToTable("Order");

            orderConfiguration.HasKey(x => x.Id);

            orderConfiguration.Property<DateTime>("Date").IsRequired();
            orderConfiguration.Property<int>("BuyerId").IsRequired();

            orderConfiguration.HasMany(x => x.OrderItems);
        }
    }
}
