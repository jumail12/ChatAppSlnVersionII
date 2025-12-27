using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Cmd
{
    public class LeaveRoomMemberCmd : IRequest<IApiResult>
    {
        public string? p_room_id { get; set; }
        public string? p_user_id { get; set; }
    }

    public class LeaveRoomMemberCmdHandler : IRequestHandler<LeaveRoomMemberCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public LeaveRoomMemberCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<IApiResult> Handle(LeaveRoomMemberCmd request, CancellationToken cancellationToken)
        {
            var para= new Dapper.DynamicParameters();
            para.Add("@p_rd_room_id", request.p_room_id);
            para.Add("@p_rd_user_id", request.p_user_id);
            var psql= "select fn_leave_room_member(@p_rd_room_id, @p_rd_user_id);";
            var res=await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);

            return new SucessResult<string>("User has left the room.")
            {
                ResultType = ResultType.Success,
                Message = "Success",
                Data = "User has left the room.",
            };
        }
    }
}
