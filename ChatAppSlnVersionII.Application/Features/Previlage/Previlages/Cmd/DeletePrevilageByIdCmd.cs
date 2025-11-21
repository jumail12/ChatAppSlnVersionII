using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.Previlages.Cmd
{
    public record DeletePrevilageByIdCmd(string? prv_id,string? p_user): IRequest<IApiResult>;

    public class DeletePrevilageByIdCmdHandler : IRequestHandler<DeletePrevilageByIdCmd, IApiResult>
    {
        private readonly Domain.Interfaces.IDataAccess _dataAccess;
        public DeletePrevilageByIdCmdHandler(Domain.Interfaces.IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(DeletePrevilageByIdCmd request, CancellationToken cancellationToken)
        {
            var para = new Dapper.DynamicParameters();
            para.Add("@p_prv_id", request.prv_id);
            para.Add("@p_user", request.p_user);
            var psql = "select delete_privilege_by_id(@p_prv_id,@p_user);";
            var res = await _dataAccess.ExecuteAsync(psql, para, false);
            return new BaseApiExeResult
            {
                Message = "Success",
                ResultType = ResultType.Success
            };
        }
    }
}
