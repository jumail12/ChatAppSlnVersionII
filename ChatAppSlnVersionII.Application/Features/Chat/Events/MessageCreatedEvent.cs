using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Events
{
    public class MessageCreatedEvent : INotification
    {
        public string? cm_message_id { get; set; }
        public string? cm_room_id { get; set; }
        public string? cm_sender_id { get; set; }
        public string? reply_to_message_id { get; set; }
        public string? cm_message_text { get; set; }
        public string? cm_media_url { get; set; }
        public string? cm_message_type { get; set; }
        public DateTime? cm_created_at { get; set; }
        public bool? is_admin { get; set; }
        public string? h_user_name { get; set; }
    }
}
