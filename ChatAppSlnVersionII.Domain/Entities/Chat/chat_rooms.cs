using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Domain.Entities.Chat
{
    public class chat_rooms
    {
        public string rh_room_id { get; set; }
        public string rh_room_name { get; set; }

        public string rh_room_type { get; set; }   // private | group | channel
        public string rh_room_owner_id { get; set; }

        public string? rh_room_description { get; set; }
        public string? rh_room_avatar_url { get; set; }

        public int? rh_max_members { get; set; }

        public bool rh_is_active { get; set; }
        public bool rh_is_deleted { get; set; }

        public string rh_created_by { get; set; }
        public DateTime rh_created_at { get; set; }

        public string? rh_updated_by { get; set; }
        public DateTime? rh_updated_at { get; set; }

        public string? rh_deleted_by { get; set; }
        public DateTime? rh_deleted_at { get; set; }
    }


    public class chat_room_members
    {
        public string? rd_member_id { get; set; }
        public string? rd_room_id { get; set; }
        public string? rd_user_id { get; set; }

        public string rd_role { get; set; }           // owner | admin | member
        public DateTime rd_joined_at { get; set; }
        public DateTime? rd_left_at { get; set; }

        public bool rd_is_muted { get; set; }
        public bool rd_is_banned { get; set; }

        public string rd_created_by { get; set; }
        public DateTime rd_created_at { get; set; }

        public string? rd_updated_by { get; set; }
        public DateTime? rd_updated_at { get; set; }

        public string? rd_deleted_by { get; set; }
        public DateTime? rd_deleted_at { get; set; }
    }


    public class chat_messages
    {
        public string cm_message_id { get; set; }

        public string cm_room_id { get; set; }
        public string cm_sender_id { get; set; }

        public string cm_message_type { get; set; }    // text | image | file | system
        public string? cm_message_text { get; set; }
        public string? cm_media_url { get; set; }

        public string? reply_to_message_id { get; set; }

        public bool cm_is_edited { get; set; }
        public bool cm_is_deleted { get; set; }

        public string cm_created_by { get; set; }
        public DateTime cm_created_at { get; set; }

        public string? cm_updated_by { get; set; }
        public DateTime? cm_updated_at { get; set; }

        public string? cm_deleted_by { get; set; }
        public DateTime? cm_deleted_at { get; set; }
    }



}
