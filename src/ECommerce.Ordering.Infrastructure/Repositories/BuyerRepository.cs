using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Buyer>> GetAsync()
        {
            return await _orderingContext.Buyers.ToListAsync();
        }
    }
}