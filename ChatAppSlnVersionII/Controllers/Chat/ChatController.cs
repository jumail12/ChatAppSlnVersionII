using ChatAppSlnVersionII.Application.Features.Chat.Cmd;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatAppSlnVersionII.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrUpdate")]
        [Authorize]
        public async Task<IApiResult> CreateOrUpdateChatRoom([FromBody] CreateOrUpdateChatRoomCmd cmd)
        {
            string p_user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cmd.rh_created_by = p_user;
            var result = await _mediator.Send(cmd);
            return result;
        }

        [HttpPost("CreateMember")]
        [Authorize]
        public async Task<IApiResult> CreateRoomMember([FromBody] CreateRoomMemberCmd cmd)
        {
            string p_user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            cmd.p_user = p_user;
            cmd.rd_user_id = p_user;
            var result = await _mediator.Send(cmd);
            return result;
        }

        [HttpPost("LeaveRoom")]
        [Authorize]
        public async Task<IApiResult> LeaveRoom([FromQuery]string? room_id)
        {
            string p_user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cmd = new LeaveRoomMemberCmd
            {
                p_room_id = room_id,
                p_user_id = p_user
            };
            var result = await _mediator.Send(cmd);
            return result;
        }
    }
}
