using ChatAppSlnVersionII.Application.Dtos.ChatDtos;
using ChatAppSlnVersionII.Application.Dtos.ModuleDocDtos;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Query
{
    public record GetRoomHeadersQuery(string? rh_room_id,string? search) : IRequest<IApiResult>;


    public class GetRoomHeadersQueryHandler : IRequestHandler<GetRoomHeadersQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public GetRoomHeadersQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(GetRoomHeadersQuery request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_rh_room_id",string.IsNullOrEmpty( request.rh_room_id)?"": request.rh_room_id);
            para.Add("@p_search", string.IsNullOrEmpty(request.search)?"": request.search);

            var sql = @"SELECT * FROM fn_get_room_headers(@p_rh_room_id,@p_search);";

            var res = await _dataAccess.ExecuteListAsync<chat_roomsDto>(sql, para, false);
            return new SucessResult<List<chat_roomsDto>>(res)
            {
                Data = res,
                Message = res.Count > 0 ? "Success" : "No data",
                ResultType = res.Count > 0 ? ResultType.Success : ResultType.NoData,
            };
            throw new NotImplementedException();
        }
    }
}
