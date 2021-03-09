using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eShop.BackendServer.Models.ViewModels.Systems
{
    public class FunctionCreateRequestValidator : AbstractValidator<FunctionCreateRequest>
    {
        public FunctionCreateRequestValidator(IStringLocalizer<FunctionCreateRequest> localizer)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(x=> localizer["Id value is required"])
                .MaximumLength(50).WithMessage(x => localizer["Function Id cannot over limit 50 characters"]);

            RuleFor(x => x.NameTemp).NotEmpty().WithMessage(x => localizer["Name value is required"])
                .MaximumLength(200).WithMessage(x => localizer["Name cannot over limit 200 characters"]);

            RuleFor(x => x.Url).NotEmpty().WithMessage(x => localizer["URL value is required"])
                .MaximumLength(200).WithMessage(x => localizer["URL cannot over limit 200 characters"]);

            RuleFor(x => x.ParentId).MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.ParentId))
                .WithMessage(x => localizer["URL cannot over limit 50 characters"]);
        }
    }
}
