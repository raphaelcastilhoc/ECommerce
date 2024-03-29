﻿using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Ordering.Infrastructure.EntityConfigurations
{
    public class BuyerEntityConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> orderConfiguration)
        {
            orderConfiguration.ToTable("Buyer");

            orderConfiguration.HasKey(x => x.Id);

            orderConfiguration.Property<string>("Name").IsRequired();
        }
    }
}
