using MediatR;

namespace ECommerce.Ordering.Api.Application.Commands
{
    public class AddOrderCommand : IRequest
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int BuyerId { get; set; }
    }
}
