using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;


namespace ChatAppSlnVersionII.Application.Features.RoleGroup.CmdHandlers
{
    public class CreateOrUpdateRoleGrpCmd : IRequest<IApiResult>
    {
        public string? role_id { get; set; }
        public string? role_type { get; set; }
        public string? role_description { get; set; }
        public string? p_createdUser { get; set; }
    }

    public class CreateOrUpdateRoleGrpHandler : IRequestHandler<CreateOrUpdateRoleGrpCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public CreateOrUpdateRoleGrpHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(CreateOrUpdateRoleGrpCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_role_id", request.role_id);
            para.Add("@p_role_type", request.role_type);
            para.Add("@p_role_description", request.role_description);
            para.Add("@p_user", request.p_createdUser);
            var sql = "SELECT create_or_update_rolegroup(@p_role_id, @p_role_type, @p_role_description, @p_user);";
            var res = await _dataAccess.ExecuteScalarAsync<string>(sql, para, false);
            return new SucessResult<string>(res.ToString())
            {
                Message="Success",
                ResultType=ResultType.Success,
                Data=res.ToString() 
            };
        }
    }
}
