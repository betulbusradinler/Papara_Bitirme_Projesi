using ExpenseTracker.Schema;
using FluentValidation;

namespace ExpenseTracker.Api.Validation;

public class AuthValidator : AbstractValidator<AuthRequest>
{
    public AuthValidator()
    {
        RuleFor(x => x.UserName).MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Password).MinimumLength(5).MaximumLength(50);
    }
}
