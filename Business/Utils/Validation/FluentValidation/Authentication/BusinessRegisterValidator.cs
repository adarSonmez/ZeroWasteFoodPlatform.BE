using Domain.DTOs.Authentication;
using FluentValidation;

namespace Business.Utils.Validation.FluentValidation.Authentication;

public class BusinessRegisterValidator : AbstractValidator<BusinessRegisterDto>
{
    public BusinessRegisterValidator()
    {
        # region Required

        RuleFor(u => u.Name).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(u => u.Name).MaximumLength(127).WithMessage("Name cannot be longer than 127 characters");
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
        RuleFor(u => u.Email).MaximumLength(127).WithMessage("Email cannot be longer than 127 characters");
        RuleFor(u => u.Email).EmailAddress().WithMessage("Email is not valid");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
        RuleFor(u => u.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage(
                "NewPassword must be at least 8 characters, contain at least one uppercase letter, one lowercase letter, one number, and one special character");
        RuleFor(u => u.Username).NotEmpty().WithMessage("Username cannot be empty");
        RuleFor(u => u.Username).MaximumLength(127).WithMessage("Username cannot be longer than 127 characters");
        RuleFor(u => u.Username).Matches(@"^[a-zA-Z0-9]+$")
            .WithMessage("Username can only contain alphanumeric characters");
        RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("PhoneNumber cannot be empty");
        RuleFor(u => u.PhoneNumber).Matches(@"^(\+[0-9]{1,3})?[0-9]{10,11}$")
            .WithMessage("PhoneNumber is not valid");
        RuleFor(u => u.Address).NotEmpty().WithMessage("Address cannot be empty");
        RuleFor(u => u.Address).MaximumLength(1023).WithMessage("Address cannot be longer than 1023 characters");

        #endregion Required

        # region Optional

        RuleFor(u => u.Website).MaximumLength(127).WithMessage("Website cannot be longer than 127 characters");
        RuleFor(u => u.Description).MaximumLength(2047)
            .WithMessage("Description cannot be longer than 2047 characters");
        RuleFor(u => u.Logo).MaximumLength(1023).WithMessage("Logo cannot be longer than 1023 characters");
        RuleFor(u => u.CoverPhoto).MaximumLength(1023).WithMessage("CoverPhoto cannot be longer than 1023 characters");

        # endregion Optional
    }
}