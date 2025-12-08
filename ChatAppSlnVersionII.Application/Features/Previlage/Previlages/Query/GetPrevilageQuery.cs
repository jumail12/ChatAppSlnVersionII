using ChatAppSlnVersionII.Application.Dtos.Previlage;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Previlage.Previlages.Query
{
    public record GetPrevilageQuery(string? prv_id,string? prv_grpid,string? p_search) : IRequest<IApiResult>;

    public class GetPrevilageQueryHandler : IRequestHandler<GetPrevilageQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public GetPrevilageQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(GetPrevilageQuery request, CancellationToken cancellationToken)
        {
            var para = new Dapper.DynamicParameters();
            para.Add("@p_prv_id", request.prv_id);
            para.Add("@p_prv_grpid", request.prv_grpid);
            para.Add("@p_search", request.p_search);
            var psql = "select * from get_previlages(@p_prv_id,@p_prv_grpid,@p_search);";
            var res =await  _dataAccess.ExecuteListAsync<PrevilageDto>(psql, para, false);
            return new SucessResult<List<PrevilageDto>>(res)
            {
                Data = res,
                Message = res.Count > 0 ? "Success" : "No data",
                ResultType = res.Count > 0 ? ResultType.Success : ResultType.NoData,
            };
        }
    }
}
