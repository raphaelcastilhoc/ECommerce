using System.Threading.Tasks;

namespace ECommerce.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task SaveAsync();
    }
}
