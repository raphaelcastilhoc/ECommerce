using ECommerce.Inventory.Domain.Aggregates.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Api.Application.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResult>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetProductByIdQueryResult(1, "Product1", 5));
        }
    }
}
