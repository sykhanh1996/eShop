using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eShop.BackendServer.Models.ViewModels.Systems
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator(IStringLocalizer<UserCreateRequest> localizer)
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(x => localizer["User name is required"]);

            RuleFor(x => x.Password).NotEmpty().WithMessage(x => localizer["Password is required"])
                .MinimumLength(8).WithMessage(x => localizer["Password has to at least 8 characters"])
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage(x => localizer["Password is not match complexity rules"]);

            RuleFor(x => x.Email).NotEmpty().WithMessage(x => localizer["Email is required"])
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage(x => localizer["Email format is not match"]);

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(x => localizer["PhoneNumber"]);

            RuleFor(x => x.FirstName).NotEmpty().WithMessage(x => localizer["First name is required"])
                .MaximumLength(50).WithMessage(x => localizer["First name can not over 50 characters limit"]);

            RuleFor(x => x.LastName).NotEmpty().WithMessage(x => localizer["Last name is required"])
                .MaximumLength(50).WithMessage(x => localizer["Last name can not over 50 characters limit"]);
        }
    }
}
