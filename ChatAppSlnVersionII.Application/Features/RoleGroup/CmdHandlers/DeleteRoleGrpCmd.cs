using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.RoleGroup.CmdHandlers
{
    public class DeleteRoleGrpCmd : IRequest<IApiResult>
    {
        public string? role_id {  get; set; }
    }

    public class DeleteRoleGrpCmdHandler : IRequestHandler<DeleteRoleGrpCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public DeleteRoleGrpCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(DeleteRoleGrpCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_role_id", request.role_id);
            para.Add("@p_user", "Admin");

            var res = await _dataAccess.ExecuteAsync("DeleteRoleGroupById", para, true);
            return new BaseApiExeResult
            {
                Message="Susccess",
                ResultType=ResultType.Success,
            };
        }
    }
}
