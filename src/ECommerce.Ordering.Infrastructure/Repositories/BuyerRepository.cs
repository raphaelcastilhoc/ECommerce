using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Infrastructure.Repositories
{
    public class BuyerRepository : Repository<Buyer>, IBuyerRepository
    {
        private readonly OrderingContext _orderingContext;

        public BuyerRepository(OrderingContext orderingContext) : base(orderingContext)
        {
            _orderingContext = orderingContext;
        }

        public async Task AddAsync(Buyer buyer)
        {
            await _orderingContext.AddAsync(buyer);
        }
    }
}