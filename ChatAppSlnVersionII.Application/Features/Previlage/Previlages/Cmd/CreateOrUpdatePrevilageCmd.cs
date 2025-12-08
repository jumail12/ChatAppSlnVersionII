using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.Previlages.Cmd
{
    public class CreateOrUpdatePrevilageCmd : IRequest<IApiResult>
    {
        public string? prv_id { get; set; }
        public string? prv_prgid { get; set; }
        public string? prv_privilegeName { get; set; }
        public string? prv_description { get; set; }
        public string? p_user { get; set; }
    }

    public class CreateOrUpdatePrevilageCmdHandler : IRequestHandler<CreateOrUpdatePrevilageCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public CreateOrUpdatePrevilageCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(CreateOrUpdatePrevilageCmd request, CancellationToken cancellationToken)
        {
            var para = new Dapper.DynamicParameters();
            para.Add("@p_prv_id", request.prv_id);
            para.Add("@p_prv_prgid", request.prv_prgid);
            para.Add("@p_prv_privilegeName", request.prv_privilegeName);
            para.Add("@p_prv_description", request.prv_description);
            para.Add("@p_user", request.p_user);
            var psql = "select create_or_update_privileges(@p_prv_id,@p_prv_prgid,@p_prv_privilegeName,@p_prv_description,@p_user);";
            var res = await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);
            return new SucessResult<string>(res)
            {
                Data = res.ToString(),
                Message = "Success",
                ResultType = ResultType.Success,
            };
        }
    }


}
