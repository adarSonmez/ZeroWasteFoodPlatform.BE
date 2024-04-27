using Domain.DTOs.Marketing;
using FluentValidation;

namespace Business.Utils.Validation.FluentValidation.Marketing;

internal class MonitoredProductAddValidator : AbstractValidator<MonitoredProductAddDto>
{
    internal MonitoredProductAddValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MinimumLength(2).MaximumLength(127)
            .WithMessage("Name should be between 2 and 127 characters");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Description).MaximumLength(1023)
            .WithMessage("Description cannot be longer than 1023 characters");
        RuleFor(x => x.Photo).MaximumLength(2047).WithMessage("Photo cannot be longer than 2047 characters");
        RuleFor(x => x.ExpirationDate).NotEmpty().WithMessage("Expiration date is required");
        RuleFor(x => x.CategoriesIds).NotEmpty().WithMessage("Categories are required");
        RuleFor(x => x.CategoriesIds).Must(x => x.Count > 0).WithMessage("Product must have at least one category");
    }
}