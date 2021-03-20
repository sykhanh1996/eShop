using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.BackendServer.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace eShop.BackendServer.Models.ViewModels.Contents
{
    public class CategoryCreateRequestValidator : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryCreateRequestValidator(IStringLocalizer<CategoryCreateRequest> localizer,IString formatstring)
        {
            RuleFor(x => x.NameVn).MaximumLength(255).WithMessage(x => formatstring.ReturnString(localizer["String cannot over limit number characters"], "NameVn","255"));
            RuleFor(x => x.SeoPageTitleVn).MaximumLength(255).WithMessage(x => formatstring.ReturnString(localizer["String cannot over limit number characters"], "SeoPageTitleVn", "255"));
            RuleFor(x => x.SeoAliasVn).MaximumLength(255).WithMessage(x => formatstring.ReturnString(localizer["String cannot over limit number characters"], "SeoAliasVn", "255"));
            RuleFor(x => x.SeoKeywordsVn).MaximumLength(255).WithMessage(x => formatstring.ReturnString(localizer["String cannot over limit number characters"], "SeoKeywordsVn", "255"));
            RuleFor(x => x.SeoDescriptionVn).MaximumLength(255).WithMessage(x => formatstring.ReturnString(localizer["String cannot over limit number characters"], "SeoDescriptionVn", "255"));
        }
    }
}
