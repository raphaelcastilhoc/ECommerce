using ECommerce.Ordering.Api.Application.DTOs;
using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.Commands
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public AddOrderCommandHandler(IOrderRepository orderRepository,
            IHttpClientFactory httpClientFactory)
        {
            _orderRepository = orderRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("Inventory");
            var response = await client.GetAsync($"Products/{request.ProductId}");
            response.EnsureSuccessStatusCode();

            var product = await response.Content.ReadAsAsync<ProductDTO>();


            //External request to Inventory Service to check quantity

            return Unit.Value;
        }
    }
}
