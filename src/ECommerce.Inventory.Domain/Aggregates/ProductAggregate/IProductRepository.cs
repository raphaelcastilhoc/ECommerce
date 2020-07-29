using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Domain.Aggregates.ProductAggregate
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();

        Task AddAsync(Product product);
    }
}
