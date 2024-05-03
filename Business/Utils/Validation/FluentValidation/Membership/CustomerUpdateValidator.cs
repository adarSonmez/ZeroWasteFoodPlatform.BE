using Domain.DTOs.Membership;
using FluentValidation;

namespace Business.Utils.Validation.FluentValidation.Membership;

public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDto>
{
    public CustomerUpdateValidator()
    {
        # region Required

        RuleFor(x => x.Id).NotEmpty().WithMessage("Customer Id is required");

        #endregion Required

        # region Optional

        RuleFor(x => x.FirstName).MinimumLength(2).When(x => x.FirstName != null)
            .WithMessage("First name should be at least 2 characters long");
        RuleFor(x => x.LastName).MinimumLength(2).When(x => x.LastName != null)
            .WithMessage("Last name address should be at least 2 characters long");
        RuleFor(x => x.Username).MinimumLength(3).When(x => x.Username != null)
            .WithMessage("Username should be at least 3 characters long");
        RuleFor(x => x.Username).Matches(@"^[a-zA-Z0-9]*$").When(x => x.Username != null)
            .WithMessage("Username should only contain alphanumeric characters");
        RuleFor(x => x.Email).EmailAddress().When(x => x.Email != null)
            .WithMessage("Email should be a valid email address");
        RuleFor(x => x.PhoneNumber).Matches(@"^\+(?:[0-9]●?){6,14}[0-9]$").When(x => x.PhoneNumber != null)
            .WithMessage("Phone number should be a valid phone number");
        RuleFor(x => x.UseMultiFactorAuthentication).Matches(@"^(true|false)$")
            .When(x => x.UseMultiFactorAuthentication != null)
            .WithMessage("Use multi factor authentication should be a boolean (true, false)");
        RuleFor(x => x.Avatar).Matches(@"^.*\.(jpg|jpeg|png)$").When(x => x.Avatar != null)
            .WithMessage("Avatar should be a valid image file. Supported formats are jpg, jpeg, png");

        #endregion Optional
    }
}