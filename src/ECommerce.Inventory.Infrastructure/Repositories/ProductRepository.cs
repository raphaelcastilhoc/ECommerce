using ECommerce.Inventory.Domain.Aggregates.ProductAggregate;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>("products");
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _products.AsQueryable().ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }
    }
}
