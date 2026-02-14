using ChatAppSlnVersionII.Application.Dtos.ChatDtos;
using ChatAppSlnVersionII.Application.Features.Chat.Cmd;
using ChatAppSlnVersionII.Application.Features.Chat.Query;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSlnVersionII.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateMessage")]
        [Authorize]
        public async Task<IApiResult> CreateMessage([FromBody] CreateMSGCmd cmd)
        {
            var p_user = Convert.ToString(HttpContext.Items["UserId"]);
            cmd.p_user = p_user;
            cmd.cm_sender_id = p_user;
            var result = await _mediator.Send(cmd);
            return result;
        }

        [HttpGet("GetMessagesByRoom")]
        [Authorize]
        public async Task<IApiResult> GetMessagesByRoom([FromQuery]string? roomid,int? pageNo=1,int? pagesize=20)
        {
            var p_user = Convert.ToString(HttpContext.Items["UserId"]);
            var payload= new GetMSGbyRoomQuery()
            {
                user = p_user,
                roomid = roomid,
                page = pageNo??1,
                pagesize = pagesize??20,
            };
            var result = await _mediator.Send(payload);
            return result;
        }
    }
}
