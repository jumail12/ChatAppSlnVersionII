using ChatAppSlnVersionII.Application.Dtos.ChatDtos;
using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System.Text.Json;


namespace ChatAppSlnVersionII.Application.Features.Chat.Cmd
{
    public class CreateOrUpdateChatRoomCmd : IRequest<IApiResult>
    {
        public string? rh_room_id { get; set; }
        public string? rh_room_name { get; set; }
        public string? rh_room_type { get; set; }   // private | group | channel
        public string? rh_room_owner_id { get; set; }
        public string? rh_room_description { get; set; }
        public string? rh_room_avatar_url { get; set; }
        public int? rh_max_members { get; set; }
        public string? rh_created_by { get; set; }
        public bool rh_is_active { get; set; }
    }

    public class CreateOrUpdateChatRoomCmdHandler : IRequestHandler<CreateOrUpdateChatRoomCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public CreateOrUpdateChatRoomCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(CreateOrUpdateChatRoomCmd request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_rh_room_id", request.rh_room_id);
            para.Add("@p_rh_room_name", request.rh_room_name);
            para.Add("@p_rh_room_type", request.rh_room_type);
            para.Add("@p_rh_room_owner_id", request.rh_room_owner_id);
            para.Add("@p_rh_room_description", request.rh_room_description);
            para.Add("@p_rh_room_avatar_url", request.rh_room_avatar_url);
            para.Add("@p_rh_max_members", request.rh_max_members);
            para.Add("@p_user", request.rh_created_by);

            var psql= "select sf_create_or_update_chatroom(@p_rh_room_id, @p_rh_room_name,@p_rh_room_type,@p_rh_room_owner_id,@p_rh_room_description,@p_rh_room_avatar_url,@p_rh_max_members,@p_user);";
            var res = await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);

            return new SucessResult<string>(res)
            {
                ResultType = ResultType.Success,
                Message = "Success",
                Data = res,
            };
        }
    }
}
