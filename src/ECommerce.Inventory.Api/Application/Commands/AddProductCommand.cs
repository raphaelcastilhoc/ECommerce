using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Inventory.Api.Application.Commands
{
    public class AddProductCommand : IRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
