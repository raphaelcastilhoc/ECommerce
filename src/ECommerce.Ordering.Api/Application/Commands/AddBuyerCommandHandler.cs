using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.Commands
{
    public class AddBuyerCommandHandler : IRequestHandler<AddBuyerCommand>
    {
        private readonly IBuyerRepository _buyerRepository;

        public AddBuyerCommandHandler(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public async Task<Unit> Handle(AddBuyerCommand request, CancellationToken cancellationToken)
        {
            var buyer = new Buyer(request.Name);

            await _buyerRepository.AddAsync(buyer);
            await _buyerRepository.SaveAsync();

            return Unit.Value;
        }
    }
}
