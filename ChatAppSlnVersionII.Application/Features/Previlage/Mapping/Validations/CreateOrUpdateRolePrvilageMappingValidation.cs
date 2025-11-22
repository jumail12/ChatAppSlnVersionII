using ChatAppSlnVersionII.Application.Features.Previlage.Mapping.Cmd;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.Mapping.Validations
{
    public class CreateOrUpdateRolePrvilageMappingValidation : AbstractValidator<CreateOrUpdateRolePrvilageMappingCmd>
    {
        public CreateOrUpdateRolePrvilageMappingValidation()
        {
            RuleFor(x => x.rpm_role_id)
                .NotEmpty().WithMessage("Role ID is required.")
                .MaximumLength(50).WithMessage("Role ID cannot exceed 50 characters.");
            RuleFor(x => x.rpm_privilage_id)
                .NotEmpty().WithMessage("Privilage ID is required.")
                .MaximumLength(50).WithMessage("Privilage ID cannot exceed 50 characters.");
            RuleFor(x => x.rpm_is_granted)
                .NotNull().WithMessage("Is Granted flag is required.");
        }
    }
}
