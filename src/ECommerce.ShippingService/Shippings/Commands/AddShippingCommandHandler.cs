using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;
using ECommerce.ApiInfrastructure.Extensions;
using ECommerce.ShippingService.Shippings.Infrastructure;
using MediatR;

namespace ECommerce.ShippingService.Shippings.Commands
{
    public class AddShippingCommandHandler : IRequestHandler<AddShippingCommand, Result<Guid>>
    {
        private readonly ShippingRepository _shipphingRepository;
        private readonly IMediator _mediator;

        public AddShippingCommandHandler(ShippingRepository shipphingRepository, IMediator mediator)
        {
            _shipphingRepository = shipphingRepository;
            _mediator = mediator;
        }

        public async Task<Result<Guid>> Handle(AddShippingCommand request, CancellationToken cancellationToken)
        {
            var shippingResult = Shipping.Create(request.OrderId, request.SendDate, request.ExpectedDeliveryDate, request.LoadWeight, request.Distance);

            if (shippingResult.IsFailure)
            {
                return Result.Failure<Guid>(shippingResult.Error);
            }

            await _shipphingRepository.AddAsync(shippingResult.Value);

            await _mediator.DispatchDomainEventsAsync(shippingResult.Value.DomainEvents);

            return shippingResult.Value.Id;
        }
    }
}
