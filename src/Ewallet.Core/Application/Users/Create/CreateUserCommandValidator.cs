using FluentValidation;

namespace Ewallet.Core.Application.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
         RuleFor(x=>x.DOB)

            .Must(BeAtLeastThirteenYearsAgo).WithMessage("Date of Birth must be at least 13 years ago");
    }

    private bool BeAtLeastThirteenYearsAgo(DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dob.Year;
        if (dob > today.AddYears(-age)) age--;

        return age >= 13;
    }
}
