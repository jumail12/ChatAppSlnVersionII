using ChatAppSlnVersionII.Application.Features.Chat.Cmd;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Validation
{
    public class CreateOrUpdateChatRoomValidation : AbstractValidator<CreateOrUpdateChatRoomCmd>
    {
        public CreateOrUpdateChatRoomValidation()
        {
            RuleFor(x => x.rh_room_name)
             .NotEmpty().WithMessage(" Room name is required.")
             .MaximumLength(100).WithMessage(" Room name must not exceed 100 characters.");
            RuleFor(x => x.rh_room_type)
                .NotEmpty().WithMessage(" Room type is required.")
                .Must(type => new[] { "PRIVATE", "GROUP", "CHANNEL" }.Contains(type))
                .WithMessage(" Room type must be either 'PRIVATE', 'GROUP', or 'CHANNEL'.");
            RuleFor(x => x.rh_room_owner_id)
                .NotEmpty().WithMessage(" Room owner ID is required.");
            RuleFor(x => x.rh_max_members)
                .GreaterThan(0).When(x => x.rh_room_type == "GROUP" || x.rh_room_type == "CHANNEL")
                .WithMessage(" Max members must be greater than 0 for group or channel rooms.");
        }
    }
}
