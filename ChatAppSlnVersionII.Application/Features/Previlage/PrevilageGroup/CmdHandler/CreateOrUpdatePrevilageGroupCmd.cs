using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.PrevilageGroup.CmdHandler
{
    public class CreateOrUpdatePrevilageGroupCmd : IRequest<IApiResult>
    {
        public string? prg_id { get; set; }
        public string? prg_group { get; set; }
        public string? prg_description { get;set; }
        public string? p_user { get; set; }
    }

    public class CreateOrUpdatePrevilageGroupCmdHandler : IRequestHandler<CreateOrUpdatePrevilageGroupCmd, IApiResult> 
    {
        private readonly IDataAccess _dataAccess;
        public CreateOrUpdatePrevilageGroupCmdHandler(IDataAccess dataAccess)
        {
             _dataAccess = dataAccess;  
        }

        public async Task<IApiResult> Handle(CreateOrUpdatePrevilageGroupCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();

            para.Add("@p_prg_id", request.prg_id);
            para.Add("@p_prg_group", request.prg_group);
            para.Add("@p_prg_description", request.prg_description);
            para.Add("@p_user", request.p_user);

            var psql = "select create_or_update_previlage_group(@p_prg_id,@p_prg_group,@p_prg_description,@p_user);";
            var res = await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);
            return new SucessResult<string>(res)
            {
                Data=res.ToString(),
                Message="Success",
                ResultType=ResultType.Success,
            };
        }
    }


}
