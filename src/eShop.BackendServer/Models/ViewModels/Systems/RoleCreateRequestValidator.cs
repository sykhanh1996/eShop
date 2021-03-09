using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eShop.BackendServer.Models.ViewModels.Systems
{
    public class RoleCreateRequestValidator : AbstractValidator<RoleCreateRequest>
    {
        public RoleCreateRequestValidator(IStringLocalizer<RoleCreateRequest> localizer)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(x=> localizer["Id value is required"])
                .MaximumLength(50).WithMessage(x => localizer["Role id cannot over limit 50 characters"]);

            RuleFor(x => x.Name).NotEmpty().WithMessage(x=>localizer["Role name is required"]);
        }
    }
}
