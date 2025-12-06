using ChatAppSlnVersionII.Application.Dtos.Previlage;
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

namespace ChatAppSlnVersionII.Application.Features.Previlage.Mapping.Query
{
    public record GetRolePrivilageByUserIdQuery(string? userId) : IRequest<IApiResult>;

    public class GetRolePrivilageByUserIdHandler : IRequestHandler<GetRolePrivilageByUserIdQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public GetRolePrivilageByUserIdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(GetRolePrivilageByUserIdQuery request, CancellationToken cancellationToken)
        {
           var para= new DynamicParameters();
              para.Add("@p_user_id", request.userId);
              var psql= "select * from get_roleprevilages_byuserid(@p_user_id);";
              var res=await _dataAccess.ExecuteListAsync<roleprevilages_byuserid_dto>(psql, para, false);
              return new SucessResult<List<roleprevilages_byuserid_dto>>(res)
              {
                  Data = res,
                  Message =res.Count>0? "Role Privilages fetched successfully":"No data",
                  ResultType = res.Count > 0 ? ResultType.Success : ResultType.NoData,
                  StatusCode = res.Count>0? StatusCodes.Status200OK : StatusCodes.Status204NoContent
              };
        }
    }
}
