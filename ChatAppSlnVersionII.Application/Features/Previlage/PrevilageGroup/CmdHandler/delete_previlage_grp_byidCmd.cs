using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.PrevilageGroup.CmdHandler
{
    public record delete_previlage_grp_byidCmd(string? prg_id,string? p_user) : IRequest<IApiResult>;

    public class delete_previlage_grp_byidCmdHandler : IRequestHandler<delete_previlage_grp_byidCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public delete_previlage_grp_byidCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(delete_previlage_grp_byidCmd request, CancellationToken cancellationToken)
        {
           var para = new Dapper.DynamicParameters();
            para.Add("@p_prg_id", request.prg_id);
            para.Add("@p_user", request.p_user);
            var psql = "select delete_previlage_grp_byid(@p_prg_id,@p_user);";
            var res =await  _dataAccess.ExecuteAsync(psql, para, false);
            return new BaseApiExeResult
            {
                Message = "Success",
                ResultType = ResultType.Success,
            };
        }
    }


}
