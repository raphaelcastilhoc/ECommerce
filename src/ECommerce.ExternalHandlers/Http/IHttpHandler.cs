using System.Threading.Tasks;

namespace ECommerce.ExternalHandlers.Http
{
    public interface IHttpHandler
    {
        Task<T> GetAsync<T>(string clientName, string url) where T : class;
    }
}
