using ECommerce.SeedWork;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly OrderingContext _orderingContext;

        protected Repository(OrderingContext orderingContext)
        {
            _orderingContext = orderingContext;
        }

        public async Task SaveAsync()
        {
            await _orderingContext.SaveChangesAsync();
        }
    }
}
