using ChatAppSlnVersionII.Application.Features.Module.CommandHandlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Module.Validation
{
    public class CreateUpdateValidation : AbstractValidator<CreateUpdateModuleDocCmd>
    {
        public CreateUpdateValidation() 
        {
            RuleFor(x => x.docMod)
             .NotEmpty().WithMessage(" module name is required.");
            RuleFor(x => x.docLength)
                .GreaterThan(0).WithMessage("doc lenght is not been zero");    
            RuleFor(x => x.docPrefix)
                .NotEmpty()
                .WithMessage(" docPrefix can't be null or empty");
            RuleFor(x => x.docCurrentNumber)
               .GreaterThan(0).WithMessage("docCurrentNumber not been negative");
            RuleFor(x => x.docStartNumber)
              .GreaterThan(0).WithMessage("docStartNumber not been negative");
        }
    }
}
