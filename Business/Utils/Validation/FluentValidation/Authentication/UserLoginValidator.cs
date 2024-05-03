using Domain.DTOs.Authentication;
using FluentValidation;

namespace Business.Utils.Validation.FluentValidation.Authentication;

public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
    {
        RuleFor(u => u.Email).NotEmpty().When(u => u.Username == null).WithMessage("Email cannot be empty");
        RuleFor(u => u.Username).NotEmpty().When(u => u.Email == null).WithMessage("Username cannot be empty");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
    }
}