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

namespace ChatAppSlnVersionII.Application.Features.Previlage.PrevilageGroup.QueryHandler
{
    public record PrevilageGroupQuery(string? prg_id,string? p_search) : IRequest<IApiResult>;

    public class PrevilageGroupQueryHandler : IRequestHandler<PrevilageGroupQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public PrevilageGroupQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(PrevilageGroupQuery request, CancellationToken cancellationToken)
        {
           var para= new DynamicParameters();
              para.Add("@p_prg_id", request.prg_id);
              para.Add("@p_search", request.p_search);

            var psql = "select * from get_previlage_groups(@p_prg_id,@p_search);";
            var res = await _dataAccess.ExecuteListAsync<PrevilageGroupDto>(psql, para, false);
            return new SucessResult<List<PrevilageGroupDto>>(res)
            {
                Data = res,
                Message =res.Count>0? "Success" : "No data",
                ResultType = res.Count > 0 ? ResultType.Success : ResultType.NoData,
            };
        }
    }
}
