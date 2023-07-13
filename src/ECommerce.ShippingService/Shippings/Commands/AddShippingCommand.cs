using CSharpFunctionalExtensions;
using MediatR;

namespace ECommerce.ShippingService.Shippings.Commands
{
    public class AddShippingCommand : IRequest<Result<Guid>>
    {
        public int OrderId { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        public decimal LoadWeight { get; set; }

        public decimal Distance { get; set; }
    }
}
