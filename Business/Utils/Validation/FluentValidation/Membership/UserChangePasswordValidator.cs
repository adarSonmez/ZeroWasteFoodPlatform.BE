using Domain.DTOs.Membership;
using FluentValidation;

namespace Business.Utils.Validation.FluentValidation.Membership;

internal class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
{
    internal UserChangePasswordValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("User Id is required");
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is required");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New password is required");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required");
        RuleFor(x => x.NewPassword).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$")
            .WithMessage(
                "Password should be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character");
    }
}