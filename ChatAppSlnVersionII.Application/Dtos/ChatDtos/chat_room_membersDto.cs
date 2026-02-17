using ChatAppSlnVersionII.Domain.Entities.Chat;

namespace ChatAppSlnVersionII.Application.Dtos.ChatDtos
{
    public class chat_room_membersDto
    {
        public string? rd_member_id { get; set; }
        public string? rd_room_id { get; set; }
        public string? rd_user_id { get; set; }

        public string? rd_role { get; set; }           // owner | admin | member
        public DateTime rd_joined_at { get; set; }
        public DateTime? rd_left_at { get; set; }

        public bool rd_is_muted { get; set; }
        public bool rd_is_banned { get; set; }
    }

    public class chat_roomsDto
    {
        public string? rh_room_id { get; set; }
        public string? rh_room_name { get; set; }

        public string? rh_room_type { get; set; }   // private | group | channel
        public string? rh_room_owner_id { get; set; }

        public string? rh_room_description { get; set; }
        public string? rh_room_avatar_url { get; set; }

        public int? rh_max_members { get; set; }

        public bool rh_is_active { get; set; }
        public int rh_total_active_members { get; set; } = 0;
        
    }

    public class RoomDetailResponseDto
    {
        public chat_roomsDto? room_info { get; set; }
        public List<chat_room_membersDto>? members { get; set; }
    }
}
