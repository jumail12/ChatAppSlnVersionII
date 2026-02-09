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

    public class CreateRoomMemberValidation : AbstractValidator<CreateRoomMemberCmd>
    {
        public CreateRoomMemberValidation()
        {
            RuleFor(x => x.rd_room_id)
                .NotEmpty().WithMessage(" Room ID is required.");
            RuleFor(x => x.rd_user_id)
                .NotEmpty().WithMessage(" User ID is required.");
            RuleFor(x => x.rd_role)
                .NotEmpty().WithMessage(" Role is required.")
                .Must(role => new[] {"ADMIN", "MEMBER" }.Contains(role))
                .WithMessage(" Role must be either 'ADMIN', or 'MEMBER'.");
        }
    }


    public class CreateRoomMSGValidation : AbstractValidator<CreateMSGCmd>
    {
        public CreateRoomMSGValidation()
        {
            RuleFor(x => x.cm_room_id)
                .NotEmpty()
                .WithMessage("Room ID is required.");

            RuleFor(x => x.cm_sender_id)
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor(x => x.cm_message_type)
                .NotEmpty()
                .WithMessage("Message type is required.")
                .Must(type => new[] { "TEXT", "URL" }.Contains(type))
                .WithMessage("Message type must be 'TEXT' or 'URL'.");

            RuleFor(x => x.cm_message_text)
                .NotEmpty()
                .WithMessage("Message text cannot be empty for TEXT type.")
                .When(x => x.cm_message_type == "TEXT");

            RuleFor(x => x.cm_media_url)
                .NotEmpty()
                .WithMessage("Media URL is required for URL type.")
                .When(x => x.cm_message_type == "URL");
        }
    }

}
