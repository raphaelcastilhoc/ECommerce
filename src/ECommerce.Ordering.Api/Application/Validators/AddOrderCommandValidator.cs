using ECommerce.Ordering.Api.Application.Commands;
using FluentValidation;

namespace ECommerce.Ordering.Api.Application.Validators
{
    public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderCommandValidator()
        {
            RuleFor(a => a.Quantity)
                .GreaterThan(0);
        }
    }
}
