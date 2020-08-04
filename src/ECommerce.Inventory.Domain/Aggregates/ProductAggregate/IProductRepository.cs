using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Domain.Aggregates.ProductAggregate
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string id);

        Task AddAsync(Product product);
    }
}
