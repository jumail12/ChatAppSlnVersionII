using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Query
{
    public record IsRoomMemberExistsQuery(string? rh_roomid, string? rh_userid) : IRequest<IApiResult>;

    public class IsRoomMemberExistsQueryHandler : IRequestHandler<IsRoomMemberExistsQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public IsRoomMemberExistsQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(IsRoomMemberExistsQuery request, CancellationToken cancellationToken)
        {
            var para = new Dapper.DynamicParameters();
            para.Add("@p_rh_roomid", request.rh_roomid);
            para.Add("@p_rh_userid", request.rh_userid);
            var psql = "select is_room_member_exists(@p_rh_roomid, @p_rh_userid);";

            var res = await _dataAccess.ExecuteScalarAsync<bool>(psql, para, false);
            return new SucessResult<bool>(res)
            {
                ResultType = ResultType.Success,
                Message = "Success",
                Data = res,
            };
        }
    }
}
