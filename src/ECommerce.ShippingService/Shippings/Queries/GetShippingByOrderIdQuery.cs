using CSharpFunctionalExtensions;
using MediatR;

namespace ECommerce.ShippingService.Shippings.Queries
{
    public class GetShippingByOrderIdQuery : IRequest<GetShippingByOrderIdQueryResult>
    {
        public GetShippingByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }

    public class GetShippingByOrderIdQueryResult
    {
        public GetShippingByOrderIdQueryResult(int orderId, DateTime sendDate, DateTime expectedDeliveryDate, decimal price)
        {
            OrderId = orderId;
            SendDate = sendDate;
            ExpectedDeliveryDate = expectedDeliveryDate;
            Price = price;
        }

        public int OrderId { get; }

        public DateTime SendDate { get; }

        public DateTime ExpectedDeliveryDate { get; }

        public decimal Price { get; }
    }
}
