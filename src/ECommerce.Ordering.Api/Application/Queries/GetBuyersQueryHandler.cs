using ECommerce.Ordering.Domain.Aggregates.BuyerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.Queries
{
    public class GetBuyersQueryHandler : IRequestHandler<GetBuyersQuery, IEnumerable<GetBuyersQueryResult>>
    {
        private readonly IBuyerRepository _buyerRepository;

        public GetBuyersQueryHandler(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public async Task<IEnumerable<GetBuyersQueryResult>> Handle(GetBuyersQuery request, CancellationToken cancellationToken)
        {
            var buyers = await _buyerRepository.GetAsync();
            var buyersQueryResult = Map(buyers);
            return buyersQueryResult;
        }

        public IEnumerable<GetBuyersQueryResult> Map(IEnumerable<Buyer> buyers)
        {
            foreach (var buyer in buyers)
            {
                yield return new GetBuyersQueryResult(buyer.Id, buyer.Name);
            }
        }
    }
}
