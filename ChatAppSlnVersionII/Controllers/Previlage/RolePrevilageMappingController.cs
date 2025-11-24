using ChatAppSlnVersionII.Application.Features.Previlage.Mapping.Cmd;
using ChatAppSlnVersionII.Application.Features.Previlage.Mapping.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSlnVersionII.Controllers.Previlage
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePrevilageMappingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RolePrevilageMappingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("createOrUpdate")]
        public async Task<IActionResult> CreateOrUpdateRolePrvilageMapping([FromBody] CreateOrUpdateRolePrvilageMappingCmd data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpGet("getByUserId")]
        public async Task<IActionResult> GetRolePrvilageByUserId([FromQuery] string? userId)
        {
            var result = await _mediator.Send(new GetRolePrivilageByUserIdQuery(userId));
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteRolePrvilageMappingById([FromQuery] DeleteRolePrvilageMappingByIdCmd data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
