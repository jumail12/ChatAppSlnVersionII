using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http;


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

            var psql= "select delete_role_group_byid(@p_role_id, @p_user);";

            var res = await _dataAccess.ExecuteAsync(psql, para, false);
            return new BaseApiExeResult
            {
                Message="Susccess",
                ResultType=ResultType.Success,
            };
        }
    }
}
