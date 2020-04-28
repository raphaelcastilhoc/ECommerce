using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.Commands
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        public async Task<Unit> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            //External request to Inventory Service to check quantity

            return Unit.Value;
        }
    }
}
