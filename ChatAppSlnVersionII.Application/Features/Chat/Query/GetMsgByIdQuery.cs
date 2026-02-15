using ChatAppSlnVersionII.Application.Dtos.ChatDtos;
using ChatAppSlnVersionII.Domain.Interfaces;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Query
{
    public class GetMsgByIdQuery : IRequest<ResChatMesaageDto>
    {
        public string? message_id { get; set; }
        public string? room_id { get; set; }
    }

    public class GetMsgByIdQueryHandler : IRequestHandler<GetMsgByIdQuery, ResChatMesaageDto>
    {
        private readonly IDataAccess _dataAccess;
        public GetMsgByIdQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<ResChatMesaageDto> Handle(GetMsgByIdQuery request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_messageid", request.message_id);
            para.Add("@p_roomid", request.room_id);
            var psql = "select * from sf_fetchmessage_byid(@p_messageid,@p_roomid);";
            var res = await _dataAccess.ExecuteListAsync<ResChatMesaageDto>(psql, para, false);
            return res.FirstOrDefault()?? new ResChatMesaageDto();
        }
    }
}
