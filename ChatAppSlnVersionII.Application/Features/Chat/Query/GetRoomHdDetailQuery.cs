using ChatAppSlnVersionII.Application.Dtos.ChatDtos;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using System.Text.Json;


namespace ChatAppSlnVersionII.Application.Features.Chat.Query
{
    public record GetRoomHdDetailQuery(string? p_room_id) : IRequest<IApiResult>;

    public class GetRoomHdDetailQueryHandler : IRequestHandler<GetRoomHdDetailQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public GetRoomHdDetailQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(GetRoomHdDetailQuery request, CancellationToken cancellationToken)
        {
            var para = new Dapper.DynamicParameters();
            para.Add("@p_room_id", request.p_room_id);
            var psql = "select * from fn_get_room_details_json(@p_room_id);";
            var res =await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);
            var data= JsonSerializer.Deserialize<RoomDetailResponseDto>(res ?? "{}");
            return new SucessResult<RoomDetailResponseDto?>(data)
            {
                ResultType = ResultType.Success,
                Message = "Success",
                Data = data,
            };
        }
    }
}
