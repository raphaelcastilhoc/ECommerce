using ECommerce.SeedWork;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task AddAsync(Order order);
    }
}
