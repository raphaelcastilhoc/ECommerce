using ECommerce.Inventory.Domain.Aggregates.ProductAggregate;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task AddAsync(Product product)
        {
            throw new System.NotImplementedException();
        }
    }
}
