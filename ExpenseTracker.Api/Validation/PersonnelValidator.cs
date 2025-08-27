using ExpenseTracker.Schema;
using FluentValidation;
public class PersonnelRequestValidator : AbstractValidator<PersonnelRequest>
{
    public PersonnelRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3);
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(role => role == "Admin" || role == "Personnel")
            .WithMessage("Role yalnızca 'Admin' veya 'Personnel' olabilir.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Iban)
            .NotEmpty()
            .Length(26);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
        RuleFor(x => x.PasswordConfirm)
            .Equal(x => x.Password);
        RuleFor(x => x.PersonnelAddressRequest)
            .SetValidator(new PersonnelAddressValidator());
        RuleFor(x => x.PersonnelPhoneRequest)
            .SetValidator(new PersonnelPhoneValidator());
    }
}

public class PersonnelAddressValidator : AbstractValidator<PersonnelAddressRequest>
{
    public PersonnelAddressValidator()
    {
        RuleFor(x => x.CountryCode).NotEmpty()
        .WithMessage("Country code is required")
        .Length(3).WithMessage("Country code must be exactly 3 characters");
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.District).NotEmpty();
        RuleFor(x => x.Street).NotEmpty();
        RuleFor(x => x.ZipCode).NotEmpty();
    }
}

public class PersonnelPhoneValidator : AbstractValidator<PersonnelPhoneRequest>
{
    public PersonnelPhoneValidator()
    {
        RuleFor(x => x.CountryCode).NotEmpty();
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{10,15}$")
            .WithMessage("PhoneNumber must be between 10 and 15 digits.");
    }
}
