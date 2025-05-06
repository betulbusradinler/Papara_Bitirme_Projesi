using ExpenseTracker.Schema;
using FluentValidation;

namespace ExpenseTracker.Api.Validation;
public class PersonnelExpenseReportDtoValidator : AbstractValidator<PersonnelExpenseReportDto>
{
    public PersonnelExpenseReportDtoValidator()
    {
        RuleFor(x => x.PaymentCategory)
            .NotEmpty();
        RuleFor(x => x.PaymentPoint)
            .NotEmpty();
        RuleFor(x => x.PaymentInstrument)
            .NotEmpty();
        RuleFor(x => x.Receipt)
            .NotEmpty();
        RuleFor(x => x.RejectDescription)
            .NotNull();
        RuleFor(x => x.Demand)
            .NotEmpty();
        RuleFor(x => x.Amount)
            .GreaterThan(0);
        RuleFor(x => x.PaymentDate)
            .LessThanOrEqualTo(DateTime.Today);
    }
}
public class ExpenseSummaryRequestValidator : AbstractValidator<ExpenseSummaryRequest>
{
    public ExpenseSummaryRequestValidator()
    {
        RuleFor(x => x.ReportType)
            .NotEmpty();
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(DateTime.Today)
            .When(x => x.StartDate.HasValue);
        RuleFor(x => x.EndDate)
            .LessThanOrEqualTo(DateTime.Today)
            .When(x => x.EndDate.HasValue);
        RuleFor(x => x)
            .Must(x => !(x.StartDate.HasValue && x.EndDate.HasValue) || x.StartDate <= x.EndDate);
    }
}

public class PaymentSummaryRequestValidator : AbstractValidator<PaymentSummaryRequest>
{
    public PaymentSummaryRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(DateTime.Today)
            .When(x => x.StartDate.HasValue);
        RuleFor(x => x.EndDate)
            .LessThanOrEqualTo(DateTime.Today)
            .When(x => x.EndDate.HasValue);
        RuleFor(x => x)
            .Must(x => !(x.StartDate.HasValue && x.EndDate.HasValue) || x.StartDate <= x.EndDate);
    }
}