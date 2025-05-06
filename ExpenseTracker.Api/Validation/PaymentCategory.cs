namespace ExpenseTracker.Api.Validation;

using ExpenseTracker.Schema;
using FluentValidation;

public class PaymentCategoryRequestValidator : AbstractValidator<PaymentCategoryRequest>
{
    public PaymentCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100);
    }
}
