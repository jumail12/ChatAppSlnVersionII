using ChatAppSlnVersionII.Application.Features.Previlage.PrevilageGroup.CmdHandler;
using ChatAppSlnVersionII.Application.Features.Previlage.PrevilageGroup.QueryHandler;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSlnVersionII.Controllers.Previlage
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrevilageGroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PrevilageGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createOrUpdate")]
        public async Task<IApiResult> CreateOrUpdatePRgroup([FromBody] CreateOrUpdatePrevilageGroupCmd data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<IApiResult> GetPrevilageGroups([FromQuery] PrevilageGroupQuery previlageGroup)
        {
            return await _mediator.Send(previlageGroup);
        }

        [HttpPost("delete")]    
        public async Task<IApiResult> DeletePrevilageGroup([FromQuery] delete_previlage_grp_byidCmd data)
        {
            return await _mediator.Send(data);
        }
    }
}
