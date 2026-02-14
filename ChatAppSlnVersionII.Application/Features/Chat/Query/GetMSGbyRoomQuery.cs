using ChatAppSlnVersionII.Application.Dtos.ChatDtos;
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
    public class GetMSGbyRoomQuery : IRequest<IApiResult>
    {
        public string? roomid { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
        public string user { get; set; }
    }


    public class GetMSGbyRoomQueryHandler : IRequestHandler<GetMSGbyRoomQuery, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        public GetMSGbyRoomQueryHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public async Task<IApiResult> Handle(GetMSGbyRoomQuery request, CancellationToken cancellationToken)
        {
            var para = new DynamicParameters();
            para.Add("@p_pageno", request.page);
            para.Add("@p_pagesize", request.pagesize);
            para.Add("@p_roomid", request.roomid);

            var psql = "select * from sf_fetchmessages_byroom(@p_pageno,@p_pagesize,@p_roomid);";
            var res = await _dataAccess.ExecuteListAsync<ChatMesaageDto>(psql, para, false);

            var apires=res.Select(a=> new ResChatMesaageDto
            {
                cm_message_id = a.cm_message_id,
                cm_room_id = a.cm_room_id,
                cm_sender_id = a.cm_sender_id,
                reply_to_message_id = a.reply_to_message_id,
                cm_message_text = a.cm_message_text,
                cm_media_url = a.cm_media_url,
                cm_message_type = a.cm_message_type,
                cm_created_at = a.cm_created_at,
                h_user_name=a.h_user_name,
                is_admin=a.is_admin,
            }).ToList();

            var fRes = new RESMSGFinalDto
            {
                messages = apires,
                user = request.user
            };

            return new PaginatedApiExeResult<RESMSGFinalDto>(fRes)
            {
               ResultType = ResultType.Success,
               Message = "Messages retrieved successfully",
               ResultData=new PaginationResultData<RESMSGFinalDto>
               {
                     PageNo=request.page,
                     PageSize=res.FirstOrDefault().v_total_pages??0,
                     Data= fRes
               },
            };
        }
    }
}
