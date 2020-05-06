using System.Threading.Tasks;

namespace ECommerce.Inventory.Domain.Aggregates.ProductAggregate
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
    }
}
