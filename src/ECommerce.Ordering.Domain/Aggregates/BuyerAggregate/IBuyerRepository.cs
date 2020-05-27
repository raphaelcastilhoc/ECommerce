using ECommerce.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Domain.Aggregates.BuyerAggregate
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Task AddAsync(Buyer buyer);

        Task<IEnumerable<Buyer>> GetAsync();
    }
}
