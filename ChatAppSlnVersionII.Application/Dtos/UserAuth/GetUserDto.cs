using ChatAppSlnVersionII.Application.Dtos.Previlage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Dtos.UserAuth
{
    public class GetUserDto
    {
        public string? h_user_id { get; set; }
        public string? d_user_id { get; set; }
        public string? h_user_name { get; set; }
        public string? h_user_email { get; set; }
        public string? d_avatarurl { get; set; }
        public string? d_bio { get; set; }
    }

    public class AuthGetByUserId
    {
        public string token_id { get; set; }
        public string user_id { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime exp_at { get; set; }
    }

    public class LoginResponseDto
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string userId { get; set; }
        public string email { get; set; }
        public List<roleprevilages_byuserid_dto>? prvilages { get; set; }
    }
}
