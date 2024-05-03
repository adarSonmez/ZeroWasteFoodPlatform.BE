using Domain.DTOs.Marketing;
using FluentValidation;

namespace Business.Utils.Validation.FluentValidation.Marketing;

public class MonitoredProductUpdateValidator : AbstractValidator<MonitoredProductUpdateDto>
{
    public MonitoredProductUpdateValidator()
    {
        # region Required

        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");

        #endregion Required

        # region Optional

        RuleFor(x => x.Name).MinimumLength(2).MinimumLength(127).When(x => x.Name != null)
            .WithMessage("Product name should be at least 2 characters long and at most 127 characters long");
        RuleFor(x => x.Description).MaximumLength(1023).When(x => x.Description != null)
            .WithMessage("Product description cannot be longer than 1023 characters");
        RuleFor(x => x.Photo).MaximumLength(2047).When(x => x.Photo != null)
            .WithMessage("Photo cannot be longer than 2047 characters");
        RuleFor(x => x.CategoryIds).Must(x => x?.Count > 0).When(x => x.CategoryIds != null)
            .WithMessage("Product must have at least one category");

        #endregion Optional
    }
}