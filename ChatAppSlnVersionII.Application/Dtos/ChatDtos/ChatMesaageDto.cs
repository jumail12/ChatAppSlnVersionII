using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Dtos.ChatDtos
{
    public class ChatMesaageDto
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
        public int? total_records { get; set; }
        public int? total_pages { get; set; }
    }

    public class ResChatMesaageDto
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

    public class RESMSGFinalDto
    {
        public List<ResChatMesaageDto> messages { get; set; }
        public string? user { get; set; }
    }
}
