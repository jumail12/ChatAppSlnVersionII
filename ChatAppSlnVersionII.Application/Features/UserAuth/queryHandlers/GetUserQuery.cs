using ChatAppSlnVersionII.Application.Dtos.UserAuth;
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

namespace ChatAppSlnVersionII.Application.Features.UserAuth.queryHandlers
{
    public record GetUserQuery(string? p_userId,string? p_serach) : IRequest<IApiResult>;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public GetUserQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var para= new DynamicParameters();
            para.Add("@p_h_user_id", request.p_userId);
            para.Add("@p_search", request.p_serach);

            var psql= "select * from get_user_headerdeatils(@p_h_user_id,@p_search);";
            var res=await _dataAccess.ExecuteListAsync<GetUserDto>(psql,para,false);
            return new SucessResult<List<GetUserDto>>(res)
            {
                ResultType =res.Count>0? ResultType.Success : ResultType.NoData,
                Message = res.Count > 0 ? "User details fetched successfully":"No data",
                Data = res,
            };
        }
    }
}
