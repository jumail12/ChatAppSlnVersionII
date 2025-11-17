using ChatAppSlnVersionII.Application.Dtos.Role;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.RoleGroup.QueryHandlers
{
    public class GetRoleGrpQuery : IRequest<IApiResult>
    {
        public string? p_search {  get; set; }
    }

    public class GetRoleGrpQueryHandler : IRequestHandler<GetRoleGrpQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public GetRoleGrpQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(GetRoleGrpQuery request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_search", request.p_search);
            var res = await _dataAccess.ExecuteListAsync<RoleGroupDto>("GetRoleGroups", para,true);
            return new SucessResult<List<RoleGroupDto>>(res)
            {
                Data = res,
                Message = res.Count > 0 ? "Success" : "No data",
                ResultType = res.Count > 0 ? ResultType.Success : ResultType.NoData,
            };
        }
    }
}
