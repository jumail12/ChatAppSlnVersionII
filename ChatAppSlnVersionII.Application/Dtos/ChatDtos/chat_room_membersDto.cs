using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Dtos.ChatDtos
{
    public class chat_room_membersDto
    {
        public string? rd_room_id { get; set; }
        public string? rd_user_id { get; set; }

        public string? rd_role { get; set; }           // owner | admin | member
        public DateTime rd_joined_at { get; set; }
        public DateTime? rd_left_at { get; set; }

        public bool rd_is_muted { get; set; }
        public bool rd_is_banned { get; set; }
    }
}
