using ChatAppSlnVersionII.Application.Features.Module.CommandHandlers;
using ChatAppSlnVersionII.Application.Features.Module.QuriyHandlers;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppSlnVersionII.Controllers.ModuleDoc
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleDocController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ModuleDocController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IApiResult> CreateOrUpdate([FromBody] CreateUpdateModuleDocCmd _data)
        {
            var res = await _mediator.Send(_data);
            return res;
        }

        [HttpGet("moduledocs")]
        [Authorize]
        public async Task<IApiResult> GetData([FromQuery] GetModuleDocQuery data)
        {
            var res = await _mediator.Send(data);
            return res;
        }

        [HttpPost("Delete")]
        [Authorize]
        public async Task<IApiResult> Delete([FromQuery] DeleteModuleDocByIdCmd data)
        {
            return await _mediator.Send(data);
        }
        //GenerateNextDocPK
    }
}
