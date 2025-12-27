using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Cmd
{
    public class CreateRoomMemberCmd : IRequest<IApiResult>
    {
        public string? rd_member_id { get; set; }
        public string? rd_room_id { get; set; }
        public string? rd_user_id { get; set; }
        public string? rd_role { get; set; }
        public string? p_user { get; set; }
    }

    public class CreateRoomMemberCmdHandler : IRequestHandler<CreateRoomMemberCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;

        public CreateRoomMemberCmdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(CreateRoomMemberCmd request, CancellationToken cancellationToken)
        {
            var para= new DynamicParameters();
            para.Add("@p_rd_member_id", request.rd_member_id);
            para.Add("@p_rd_room_id", request.rd_room_id);
            para.Add("@p_rd_user_id", request.rd_user_id);
            para.Add("@p_rd_role", request.rd_role);
            para.Add("@p_user", request.p_user);
            var psql= "select fn_create_room_member(@p_rd_member_id, @p_rd_room_id, @p_rd_user_id, @p_rd_role,@p_user);";
            var res = await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);
            return new SucessResult<string>(res)
            {
                ResultType = ResultType.Success,
                Message = "Success",
                Data =res,
            };
        }
    }
}
