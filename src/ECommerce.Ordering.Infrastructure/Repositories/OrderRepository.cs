using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly OrderingContext _orderingContext;

        public OrderRepository(OrderingContext orderingContext) : base(orderingContext)
        {
            _orderingContext = orderingContext;
        }

        public async Task AddAsync(Order order)
        {
            await _orderingContext.AddAsync(order);
        }
    }
}
