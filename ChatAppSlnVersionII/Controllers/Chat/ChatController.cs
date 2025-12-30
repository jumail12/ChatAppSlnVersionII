using ChatAppSlnVersionII.Application.Features.Chat.Cmd;
using ChatAppSlnVersionII.Application.Features.Chat.Query;
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
            var p_user = Convert.ToString(HttpContext.Items["UserId"]);
            cmd.rh_created_by = p_user;
            cmd.rh_room_owner_id = p_user;
            var result = await _mediator.Send(cmd);
            return result;
        }

        [HttpPost("CreateMember")]
        [Authorize]
        public async Task<IApiResult> CreateRoomMember([FromBody] CreateRoomMemberCmd cmd)
        {
            var p_user = Convert.ToString(HttpContext.Items["UserId"]);
            cmd.p_user = p_user;
            cmd.rd_user_id = p_user;
            var result = await _mediator.Send(cmd);
            return result;
        }

        [HttpPost("LeaveRoom")]
        [Authorize]
        public async Task<IApiResult> LeaveRoom([FromQuery] string? room_id)
        {
            var p_user = Convert.ToString(HttpContext.Items["UserId"]);
            var cmd = new LeaveRoomMemberCmd
            {
                p_room_id = room_id,
                p_user_id = p_user
            };
            var result = await _mediator.Send(cmd);
            return result;
        }

        [HttpPost("BanRoomMember")]
        [Authorize]
        public async Task<IApiResult> BanRoomMember([FromQuery] string? room_id, [FromQuery] string? user_id)
        {
            var p_admin = Convert.ToString(HttpContext.Items["UserId"]);
            var cmd = new BanRoomMemberByAdminCmd
            {
                p_room_id = room_id,
                p_owner_id = p_admin,
                p_user_id = user_id
            };
            var result = await _mediator.Send(cmd);
            return result;
        }

        [HttpGet("roomDetails")]
        [Authorize]
        public async Task<IApiResult> GetRoomDetails([FromQuery] string? room_id)
        {
            var result = await _mediator.Send(new GetRoomHdDetailQuery(room_id));
            return result;
        }

        [HttpGet("roomHeaders")]
        //[Authorize]
        public async Task<IApiResult> GetRoomDetails([FromQuery] GetRoomHeadersQuery getRoom)
        {
            var result = await _mediator.Send(getRoom);
            return result;
        }
    }
}
