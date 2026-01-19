using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Features.Chat.Events
{
    public class ChatRoomCreatedEvent : INotification
    {
        public string? rh_room_id { get; set; }
        public string? rh_room_name { get; set; }

        public string? rh_room_type { get; set; }   // private | group | channel
        public string? rh_room_owner_id { get; set; }

        public string? rh_room_description { get; set; }
        public string? rh_room_avatar_url { get; set; }

        public int? rh_max_members { get; set; }

        public bool rh_is_active { get; set; }
    }
}
