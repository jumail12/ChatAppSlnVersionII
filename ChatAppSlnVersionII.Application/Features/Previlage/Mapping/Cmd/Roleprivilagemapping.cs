using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.Mapping.Cmd
{
    public class CreateOrUpdateRolePrvilageMappingCmd : IRequest<IApiResult>
    {
        public string rpm_id { get; set; }
        public string rpm_role_id { get; set; }
        public string rpm_privilage_id { get; set; }
        public bool rpm_is_granted { get; set; }
        public string? user { get; set; }
    }

    public class CreateOrUpdateRolePrvilageMappingHandler : IRequestHandler<CreateOrUpdateRolePrvilageMappingCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public CreateOrUpdateRolePrvilageMappingHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(CreateOrUpdateRolePrvilageMappingCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_rpm_id", request.rpm_id);
            para.Add("@p_rpm_role_id", request.rpm_role_id);
            para.Add("@p_rpm_privilage_id", request.rpm_privilage_id);
            para.Add("@p_rpm_is_granted", request.rpm_is_granted);
            para.Add("@p_user", request.user);

            var psql= "select create_or_update_roleprvilagemapping(@p_rpm_id,@p_rpm_role_id,@p_rpm_privilage_id,@p_rpm_is_granted,@p_user);";
            var res=await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);
            return new SucessResult<string>(res)
            {
                Data = res,
                Message = "Role Privilage Mapping Created/Updated Successfully",
                ResultType = ResultType.Success
            };
        }
    }




    //-- delete role previlage mapping by id    
    public class DeleteRolePrvilageMappingByIdCmd : IRequest<IApiResult>
    {
        public string rpm_id { get; set; }
        public string? user { get; set; }
    }

    public class DeleteRolePrvilageMappingByIdHandler : IRequestHandler<DeleteRolePrvilageMappingByIdCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public DeleteRolePrvilageMappingByIdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(DeleteRolePrvilageMappingByIdCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_rpm_id", request.rpm_id);
            para.Add("@p_user", request.user);
            var psql= "select delete_role_privilage_by_id(@p_rpm_id,@p_user);";
            var res=await _dataAccess.ExecuteAsync(psql, para, false);
            return new BaseApiExeResult
            {
                Message = "Role Privilage Mapping Deleted Successfully",
                ResultType = ResultType.Success
            };
        }
    }
}
