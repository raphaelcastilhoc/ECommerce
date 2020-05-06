using ECommerce.Inventory.Domain.Aggregates.ProductAggregate;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Inventory.Api.Application.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AddProductCommandHandler> _logger;

        public AddProductCommandHandler(IProductRepository productRepository,
            ILogger<AddProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product(request.Name, request.Description, request.Quantity);
                await _productRepository.AddAsync(product);
            }
            catch(ValidationException e)
            {
                foreach (var error in e.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }

                throw;
            }

            return Unit.Value;
        }
    }
}
