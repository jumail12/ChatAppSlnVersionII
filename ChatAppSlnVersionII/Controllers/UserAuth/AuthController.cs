using ChatAppSlnVersionII.Application.Features.UserAuth.cmdHnadlers;
using ChatAppSlnVersionII.Application.Features.UserAuth.queryHandlers;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSlnVersionII.Controllers.UserAuth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrUpdateUser")]
        public async Task<IApiResult> CreateOrUpdateUser([FromBody] CreateOrUpdateUserCmd command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("UserLogin")]
        public async Task<IApiResult> UserLogin([FromBody] UserLoginCmd command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpGet]
        public async Task<IApiResult> GetUser([FromQuery] GetUserQuery data)
        {
            var result = await _mediator.Send(data);
            return result;
        }
    }
}
