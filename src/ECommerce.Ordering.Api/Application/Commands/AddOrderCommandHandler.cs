using ECommerce.ExternalHandlers.Http;
using ECommerce.Ordering.Api.Application.Constants;
using ECommerce.Ordering.Api.Application.DTOs;
using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using MediatR;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.Commands
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpHandler _httpHandler;

        public AddOrderCommandHandler(IOrderRepository orderRepository,
            IHttpHandler httpHandler)
        {
            _orderRepository = orderRepository;
            _httpHandler = httpHandler;
        }

        public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var product = await _httpHandler.GetAsync<ProductDTO>(HttpClientName.Inventory, $"Products/{request.ProductId}");

            if(product.Quantity >= request.Quantity)
            {
                var order = new Order(request.BuyerId);
                order.AddOrderItem(product.Name, request.Quantity);

                await _orderRepository.AddAsync(order);
                await _orderRepository.SaveAsync();
            }

            return Unit.Value;
        }
    }
}
