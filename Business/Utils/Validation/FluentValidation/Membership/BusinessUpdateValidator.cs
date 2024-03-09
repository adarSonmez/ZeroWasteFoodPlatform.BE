using Domain.DTOs.Membership;
using FluentValidation;

namespace Business.Utils.Validation.FluentValidation.Membership;

internal class BusinessUpdateValidator : AbstractValidator<BusinessUpdateDto>
{
    internal BusinessUpdateValidator()
    {
        # region Required

        RuleFor(x => x.Id).NotEmpty().WithMessage("Business Id is required");

        #endregion Required

        # region Optional

        RuleFor(x => x.Name).MinimumLength(3).When(x => x.Name != null)
            .WithMessage("Business name should be at least 3 characters long");
        RuleFor(x => x.Address).MinimumLength(3).When(x => x.Address != null)
            .WithMessage("Business address should be at least 3 characters long");
        RuleFor(x => x.Website).Matches(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$").When(x => x.Website != null)
            .WithMessage("Website should be a valid url");
        RuleFor(x => x.Description).MinimumLength(3).When(x => x.Description != null)
            .WithMessage("Business description should be at least 3 characters long");
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
        RuleFor(x => x.Logo).Matches(@"^.*\.(jpg|jpeg|png)$").When(x => x.Logo != null)
            .WithMessage("Logo should be a valid image file. Supported formats are jpg, jpeg, png");
        RuleFor(x => x.CoverPhoto).Matches(@"^.*\.(jpg|jpeg|png)$").When(x => x.CoverPhoto != null)
            .WithMessage("Cover photo should be a valid image file. Supported formats are jpg, jpeg, png");

        #endregion Optional
    }
}