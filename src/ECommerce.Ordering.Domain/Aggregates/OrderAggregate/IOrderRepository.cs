using ECommerce.SeedWork;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task Add(Order order);
    }
}
