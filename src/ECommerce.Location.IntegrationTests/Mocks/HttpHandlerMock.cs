using ECommerce.ExternalHandlers.Http;
using System.Threading.Tasks;

namespace ECommerce.Location.IntegrationTests.Mocks
{
    public class HttpHandlerMock : IHttpHandler
    {
        public Task<T> GetAsync<T>(string clientName, string url) where T : class
        {
            return null;
        }
    }
}