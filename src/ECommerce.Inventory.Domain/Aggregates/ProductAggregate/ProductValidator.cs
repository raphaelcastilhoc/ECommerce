using FluentValidation;

namespace ECommerce.Inventory.Domain.Aggregates.ProductAggregate
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.Name).NotNull().NotEmpty();
            RuleFor(product => product.Description).NotNull().NotEmpty();
        }
    }
}
