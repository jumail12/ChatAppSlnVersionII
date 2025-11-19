using ChatAppSlnVersionII.Application.Features.Module.CommandHandlers;
using ChatAppSlnVersionII.Application.Features.UserAuth.cmdHnadlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.UserAuth.validations
{
    public class CreateOrUpdateUserValidation  : AbstractValidator<CreateOrUpdateUserCmd>
    {
        public CreateOrUpdateUserValidation()
        {
            RuleFor(x => x.h_user_name)
             .NotEmpty().WithMessage("User name is required.");
            RuleFor(x => x.h_user_email)
                .NotEmpty().WithMessage("User email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.h_user_password)
                .NotEmpty().WithMessage("User password is required.")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
        }
    }
}
