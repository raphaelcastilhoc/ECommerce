using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.ExternalHandlers.Http
{
    public class HttpHandler : IHttpHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string clientName, string url) where T : class
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var contentResponse = await response.Content.ReadAsAsync<T>();
            return contentResponse;
        }
    }
}