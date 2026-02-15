using ChatAppSlnVersionII.Domain.Interfaces;
using ChatAppSlnVersionII.Shared.ApiResponses;
using Dapper;
using MediatR;


namespace ChatAppSlnVersionII.Application.Features.Chat.Cmd
{
    public class CreateMSGCmd : IRequest<IApiResult>
    {
        public string? cm_message_id { get; set; } = default!;
        public string? cm_room_id { get; set; } = default!;
        public string? cm_sender_id { get; set; } = default!;
        public string? reply_to_message_id { get; set; }
        public string? cm_message_text { get; set; }
        public string? cm_media_url { get; set; }
        public string? cm_message_type { get; set; }
        public bool? cm_is_edited { get; set; }
        public string? p_user { get; set; }
    }

    public class CreateMSGCmdHandler : IRequestHandler<CreateMSGCmd, IApiResult>
    {
        private readonly IDataAccess _dataAccess;
        private readonly IMediator _mediator;
        public CreateMSGCmdHandler(IDataAccess dataAccess,IMediator mediator) 
        {
          _dataAccess = dataAccess;
          _mediator = mediator;
        }
        public async Task<IApiResult> Handle(CreateMSGCmd request, CancellationToken cancellationToken)
        {
            var para= new DynamicParameters();
            para.Add("@p_cm_message_id", request.cm_message_id);
            para.Add("@p_cm_room_id", request.cm_room_id);
            para.Add("@p_cm_sender_id", request.cm_sender_id);
            para.Add("@p_reply_to_message_id", request.reply_to_message_id);
            para.Add("@p_cm_message_text", request.cm_message_text);
            para.Add("@p_cm_media_url", request.cm_media_url);
            para.Add("@p_cm_message_type", request.cm_message_type);
            para.Add("@p_user", request.p_user);
            var psql= "select sf_cou_room_message(@p_cm_message_id, @p_cm_room_id, @p_cm_sender_id, @p_reply_to_message_id, @p_cm_message_text, @p_cm_media_url, @p_cm_message_type,@p_user);";
            var res =await _dataAccess.ExecuteScalarAsync<string>(psql, para, false);

            if (!string.IsNullOrEmpty(res))
            {
                var msg = await _mediator.Send(new Query.GetMsgByIdQuery
                {
                    message_id = res,
                    room_id = request.cm_room_id,
                });

                var evt = new Events.MessageCreatedEvent
                {
                    cm_message_id = msg.cm_message_id,
                    cm_room_id = msg.cm_room_id,
                    cm_sender_id = msg.cm_sender_id,
                    reply_to_message_id = msg.reply_to_message_id,
                    cm_message_text = msg.cm_message_text,
                    cm_media_url = msg.cm_media_url,
                    cm_message_type = msg.cm_message_type,
                    cm_created_at = msg.cm_created_at,
                    h_user_name=msg.h_user_name,
                    is_admin=msg.is_admin,
                };

                await _mediator.Publish(evt, cancellationToken);
            }

            var sres=new SucessResult<string>(res)
            {
                ResultType = ResultType.Success,
                Message = "Message created successfully",
                Data = res,
            });

            return sres;
        }
    }
}
