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
}
