using ExpenseTracker.Schema;
using FluentValidation;

namespace ExpenseTracker.Api.Validation;

public class ExpenseRequestValidator : AbstractValidator<ExpenseRequest>
{
    public ExpenseRequestValidator()
    {
        RuleFor(x => x.PaymentCategoryId)
            .GreaterThan(0);
        RuleFor(x => x.ExpenseDetailRequest)
            .NotNull()
            .SetValidator(new ExpenseDetailRequestValidator());
    }
}
public class ExpenseDetailRequestValidator : AbstractValidator<ExpenseDetailRequest>
{
    public ExpenseDetailRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0);
        RuleFor(x => x.Description)
            .MaximumLength(500);
        RuleFor(x => x.PaymentPoint)
            .NotEmpty();
        RuleFor(x => x.PaymentInstrument)
            .NotEmpty();
        RuleFor(x => x.Receipt)
            .NotEmpty();
    }
}
public class ExpenseFilterRequestValidator : AbstractValidator<ExpenseFilterRequest>
{
    public ExpenseFilterRequestValidator()
    {
        When(x => x.PaymentCategoryId.HasValue, () =>
        {
            RuleFor(x => x.PaymentCategoryId.Value).GreaterThan(0);
        });
        When(x => x.DemandState.HasValue, () =>
        {
            RuleFor(x => x.DemandState.Value).InclusiveBetween(0, 2);
        });

        When(x => x.PaymentCategoryId.HasValue, () =>
       {
           RuleFor(x => x.PaymentCategoryId.Value)
               .GreaterThan(0);
       });

        When(x => x.DemandState.HasValue, () =>
        {
            RuleFor(x => x.DemandState.Value)
                .InclusiveBetween(0, 2);
        });

        When(x => x.StartDate.HasValue, () =>
        {
            RuleFor(x => x.StartDate.Value)
                .LessThanOrEqualTo(DateTime.Now);
        });

        When(x => x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate.Value)
                .LessThanOrEqualTo(DateTime.Now);
        });

        RuleFor(x => x)
            .Must(x => !(x.StartDate.HasValue && x.EndDate.HasValue) || x.StartDate <= x.EndDate);

    }
}
public class ApproveOrRejectExpenseRequestValidator : AbstractValidator<ApproveOrRejectExpenseRequest>
{
    public ApproveOrRejectExpenseRequestValidator()
    {
        RuleFor(x => x.DemandState)
            .InclusiveBetween(1, 2);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}
