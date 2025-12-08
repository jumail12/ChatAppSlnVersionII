using ChatAppSlnVersionII.Application.Dtos.ModuleDocDtos;
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

namespace ChatAppSlnVersionII.Application.Features.Module.QuriyHandlers
{
    public class GetModuleDocQuery() : IRequest<IApiResult>
    {
        public string? p_search {  get; set; }
    }

    public class GetModuleDocQueryHandler : IRequestHandler<GetModuleDocQuery, IApiResult> 
    {
        private readonly IDataAccess _dataAccess;
        public GetModuleDocQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(GetModuleDocQuery request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_search", request.p_search);

            var sql= @"SELECT * FROM get_module_doc(@p_search);";

            var res = await _dataAccess.ExecuteListAsync<GetModuleDocDto>(sql, para, false);
            return new SucessResult<List<GetModuleDocDto>>(res)
            {
                Data =res,
                Message=res.Count>0? "Success": "No data",
                ResultType = res.Count > 0 ? ResultType.Success : ResultType.NoData,
            };
        }
    }


}
