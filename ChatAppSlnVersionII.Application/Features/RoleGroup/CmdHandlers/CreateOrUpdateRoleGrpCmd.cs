using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
            para.Add("@p_role_CreatedBy", request.p_createdUser);
            para.Add(
                name: "@p_outRoleGrpId",
                value: null,
                dbType: DbType.String,
                direction: ParameterDirection.Output,
                size: 15
            );

            var res = await _dataAccess.ExecuteAsync("CreateOrUpdateRoleGroup", para, true);
            var outDoc = para.Get<string>("@p_outRoleGrpId");
            return new SucessResult<string>(outDoc)
            {
                Message="Success",
                ResultType=ResultType.Success,
                Data=outDoc
            };
        }
    }
}
