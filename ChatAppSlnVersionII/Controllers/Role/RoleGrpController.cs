using ChatAppSlnVersionII.Application.Features.RoleGroup.CmdHandlers;
using ChatAppSlnVersionII.Application.Features.RoleGroup.QueryHandlers;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSlnVersionII.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleGrpController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleGrpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IApiResult> CreateOrUpdate([FromBody] CreateOrUpdateRoleGrpCmd data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<IApiResult> GetRoleGroups([FromQuery] GetRoleGrpQuery data)
        {
            return await _mediator.Send(data);
        }

        [HttpPost("Delete")]
        public async Task<IApiResult> Delete([FromQuery] DeleteRoleGrpCmd data)
        {
            return await _mediator.Send(data);
        }
    }
}
