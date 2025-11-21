using ChatAppSlnVersionII.Application.Features.Previlage.Previlages.Cmd;
using ChatAppSlnVersionII.Application.Features.Previlage.Previlages.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSlnVersionII.Controllers.Previlage
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrevilageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PrevilageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createOrUpdate")]    
        public async Task<IActionResult> CreateOrUpdatePrevilage([FromBody] CreateOrUpdatePrevilageCmd data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPrevilages([FromQuery] GetPrevilageQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeletePrevilage([FromQuery] DeletePrevilageByIdCmd data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
