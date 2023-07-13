using ECommerce.ShippingService.Shippings.Infrastructure;
using MediatR;

namespace ECommerce.ShippingService.Shippings.Queries
{
    public class GetShippingByOrderIdQueryHandler : IRequestHandler<GetShippingByOrderIdQuery, GetShippingByOrderIdQueryResult>
    {
        private readonly ShippingRepository _shippingRepository;

        public GetShippingByOrderIdQueryHandler(ShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        public async Task<GetShippingByOrderIdQueryResult> Handle(GetShippingByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var shipping = await _shippingRepository.GetByOrderIdAsync(request.OrderId);

            var shippingQueryResult = shipping != null ?
                new GetShippingByOrderIdQueryResult(shipping.OrderId, shipping.SendDate, shipping.ExpectedDeliveryDate, shipping.Cost) : null;

            return shippingQueryResult;
        }
    }
}
