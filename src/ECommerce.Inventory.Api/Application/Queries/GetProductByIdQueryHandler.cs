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

        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            return new GetProductByIdQueryResult(product.Id.ToString(), product.Name, product.Quantity);
        }
    }
}
