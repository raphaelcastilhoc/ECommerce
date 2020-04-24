using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using ECommerce.Ordering.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Ordering.Infrastructure
{
    public class OrderingContext : DbContext
    {
        public virtual DbSet<Order> Order { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1R3EEK7\\SQLEXPRESS;Initial Catalog=sql_xp_faturamento;User id=sa;Password=Qazplm27; Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        }
    }
}
