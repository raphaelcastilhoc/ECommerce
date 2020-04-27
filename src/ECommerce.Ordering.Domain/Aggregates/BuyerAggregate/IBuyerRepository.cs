using ECommerce.SeedWork;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Domain.Aggregates.BuyerAggregate
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Task AddAsync(Buyer buyer);
    }
}
