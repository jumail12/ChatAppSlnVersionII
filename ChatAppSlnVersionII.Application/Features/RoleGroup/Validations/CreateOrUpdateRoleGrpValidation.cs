using ChatAppSlnVersionII.Application.Features.Module.CommandHandlers;
using ChatAppSlnVersionII.Application.Features.RoleGroup.CmdHandlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.RoleGroup.Validations
{
    public class CreateOrUpdateRoleGrpValidation : AbstractValidator<CreateOrUpdateRoleGrpCmd>
    {
      public CreateOrUpdateRoleGrpValidation() 
      {
           RuleFor(a=>a.role_type).NotEmpty().WithMessage("role type not been empty");
      }
             
    }
}
