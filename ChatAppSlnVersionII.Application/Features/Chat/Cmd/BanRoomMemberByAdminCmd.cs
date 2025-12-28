using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;


namespace ChatAppSlnVersionII.Application.Features.Chat.Cmd
{
    public class BanRoomMemberByAdminCmd : IRequest<IApiResult>
    {
        public string? p_room_id { get; set; }
        public string? p_user_id { get; set; }
        public string? p_owner_id { get; set; }
    }

    public class BanRoomMemberByAdminCmdHandler : IRequestHandler<BanRoomMemberByAdminCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public BanRoomMemberByAdminCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(BanRoomMemberByAdminCmd request, CancellationToken cancellationToken)
        {
            var para= new Dapper.DynamicParameters();
            para.Add("@p_rd_room_id", request.p_room_id);
            para.Add("@p_rd_user_id", request.p_user_id);
            para.Add("@p_admin_id", request.p_owner_id);
            var psql= "select fn_ban_room_member_by_admin(@p_rd_room_id, @p_rd_user_id, @p_admin_id);";
            var res=await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);
            return new SucessResult<string>("User has been banned from the room by admin.")
            {
                ResultType = ResultType.Success,
                Message = "Success",
                Data = "User has been banned from the room by admin.",
            };
        }
    }
}
