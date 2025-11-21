using ChatAppSlnVersionII.Application.Features.Previlage.Previlages.Cmd;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.Previlages.Validation
{
    public class CreateOrUpdatePrevilageValidate : AbstractValidator<CreateOrUpdatePrevilageCmd>
    {
        public CreateOrUpdatePrevilageValidate()
        {
            RuleFor(a => a.prv_prgid).NotEmpty().WithMessage("group id dont be null");
            RuleFor(a => a.prv_privilegeName).NotEmpty().WithMessage("privilege name dont be null");
            RuleFor(b => b.prv_description).NotEmpty().WithMessage("description dont been empty");
        }
    }
}
