﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace eShop.BackendServer.Models.ViewModels.Systems
{
    public class CommandAssignRequestValidator : AbstractValidator<CommandAssignRequest>
    {
        public CommandAssignRequestValidator()
        {
            RuleFor(x => x.CommandIds).NotNull()
                .WithMessage(string.Format(Messages.Required, "Mã lệnh"));

            RuleFor(x => x.CommandIds).Must(x => x.Length > 0)
                .When(x => x.CommandIds != null)
                .WithMessage("Danh sách mã lệnh phải có ít nhất 1 phần tử");

            RuleForEach(x => x.CommandIds).NotEmpty()
                .WithMessage("Danh sách mã lệnh không được chứa phần tử rỗng");
        }
    }
}
