using ECommerce.ApiInfrastructure.Extensions;
using ECommerce.ExternalHandlers.Http;
using ECommerce.Ordering.Api.Application.Constants;
using ECommerce.Ordering.Api.Application.DTOs;
using ECommerce.Ordering.Domain.Aggregates.OrderAggregate;
using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.Commands
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpHandler _httpHandler;
        private readonly IMediator _mediator;

        public AddOrderCommandHandler(IOrderRepository orderRepository,
            IHttpHandler httpHandler,
            IMediator mediator)
        {
            _orderRepository = orderRepository;
            _httpHandler = httpHandler;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var product = await _httpHandler.GetAsync<ProductDTO>(HttpClientName.Inventory, $"Products/{request.ProductId}");

            if(product.Quantity >= request.Quantity)
            {
                var order = new Order(request.BuyerId);
                order.AddOrderItem(product.Id, product.Name, request.Quantity);

                await _orderRepository.AddAsync(order);
                await _orderRepository.SaveAsync();

                await _mediator.DispatchDomainEventsAsync(order.DomainEvents);

                
            }

            return Unit.Value;
        }
    }
}
