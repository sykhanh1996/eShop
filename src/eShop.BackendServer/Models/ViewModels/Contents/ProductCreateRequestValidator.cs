using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Models.ViewModels.Systems;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eShop.BackendServer.Models.ViewModels.Contents
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator(IStringLocalizer<ProductCreateRequest> localizer)
        {
            RuleFor(x => x.ImageUrl).MaximumLength(1000).WithMessage(x => localizer["ImageUrl cannot over limit 1000 characters"]);
            RuleFor(x => x.ThumbImage).MaximumLength(1000).WithMessage(x => localizer["ThumbImage cannot over limit 1000 characters"]);
            RuleFor(x => x.Price).NotNull().WithMessage(x=> localizer["Price cannot be null"]);
            RuleFor(x => x.OriginalPrice).NotNull().WithMessage(x=> localizer["OriginalPrice cannot be null"]);
            RuleFor(x => x.NameVn).MaximumLength(255).WithMessage(x => localizer["NameVn cannot over limit 255 characters"]);
            RuleFor(x => x.DescriptionVn).MaximumLength(500).WithMessage(x => localizer["DescriptionVn cannot over limit 500 characters"]);
            RuleFor(x => x.SeoPageTitleVn).MaximumLength(255).WithMessage(x => localizer["SeoPageTitleVn cannot over limit 255 characters"]);
            RuleFor(x => x.SeoAliasVn).MaximumLength(255).WithMessage(x => localizer["SeoAliasVn cannot over limit 255 characters"]);
            RuleFor(x => x.SeoKeywordsVn).MaximumLength(255).WithMessage(x => localizer["SeoKeywordsVn cannot over limit 255 characters"]);
            RuleFor(x => x.SeoDescriptionVn).MaximumLength(255).WithMessage(x => localizer["SeoDescriptionVn cannot over limit 255 characters"]);
        }
    }
}
