using ChatAppSlnVersionII.Application.Features.UserAuth.cmdHnadlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.UserAuth.validations
{
    public class UserLoginValidation : AbstractValidator<UserLoginCmd>
    {
        public UserLoginValidation()
        {
            RuleFor(x => x.p_userEmail)
                .NotEmpty().WithMessage("User email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.p_userPassword)
                .NotEmpty().WithMessage("User password is required.")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
        }
    }
}
