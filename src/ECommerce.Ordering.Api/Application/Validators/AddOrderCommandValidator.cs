using ECommerce.Ordering.Api.Application.Commands;
using FluentValidation;

namespace ECommerce.Ordering.Api.Application.Validators
{
    public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderCommandValidator()
        {
            RuleFor(a => a.ProductId)
                .GreaterThan(0)
                .WithMessage("'ProductId' is not valid.");

            RuleFor(a => a.Quantity)
                .GreaterThan(0);

            RuleFor(a => a.BuyerId)
                .GreaterThan(0)
                .WithMessage("'BuyerId' is not valid.");
        }
    }
}
