using MediatR;

namespace ECommerce.Inventory.Api.Application.Commands
{
    public class AddProductCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
