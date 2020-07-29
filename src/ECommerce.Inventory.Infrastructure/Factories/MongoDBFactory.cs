using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ECommerce.Inventory.Infrastructure.Factories
{
    public class MongoDBFactory : IMongoDBFactory
    {
        private readonly IConfiguration _configuration;

        public MongoDBFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMongoDatabase GetDatabase()
        {
            var url = new MongoUrl(_configuration.GetValue<string>("ConnectionString"));
            var client = new MongoClient(url);
            var database = client.GetDatabase("Inventory");

            var conventionPack = new ConventionPack {
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, x => true);

            return database;
        }
    }
}
