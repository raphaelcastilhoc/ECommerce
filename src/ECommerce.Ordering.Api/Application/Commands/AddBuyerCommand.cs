using MediatR;

namespace ECommerce.Ordering.Api.Application.Commands
{
    public class AddBuyerCommand : IRequest
    {
        public string Name { get; set; }
    }
}
