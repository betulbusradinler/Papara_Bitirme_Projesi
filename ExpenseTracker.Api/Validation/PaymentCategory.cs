using ExpenseTracker.Schema;
using FluentValidation;

namespace ExpenseTracker.Api.Validation;
public class PaymentCategoryRequestValidator : AbstractValidator<PaymentCategoryRequest>
{
    public PaymentCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100);
    }
}
