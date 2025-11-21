using ChatAppSlnVersionII.Application.Features.Previlage.PrevilageGroup.CmdHandler;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.PrevilageGroup.Validations
{
    public class CreateOrUpdatePrevilageGroupValidation : AbstractValidator<CreateOrUpdatePrevilageGroupCmd>
    {
        public CreateOrUpdatePrevilageGroupValidation()
        {
            RuleFor(a=>a.prg_group).NotEmpty().WithMessage("group name dont be null");
            RuleFor(b => b.prg_description).NotEmpty().WithMessage("description dont been empty");
        }
    }
}
