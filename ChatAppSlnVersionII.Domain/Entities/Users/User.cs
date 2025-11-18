using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Domain.Entities.Users
{
    public class user_header
    {
        public string ? h_user_id { get; set; }
        public string ? h_user_name { get; set; }
        public string ? h_user_email { get; set; }
        public string ? h_user_password { get; set; }
        public bool? h_is_active { get; set; }
        public bool ? h_is_deleted { get; set; }
        public DateTime h_last_login { get; set; }
        public string ? h_user_createdby { get; set; }
        public DateTime h_user_createdat { get; set; }
        public string ? h_user_modifiedby { get; set; }
        public DateTime ? h_user_modifiedat { get; set; }
        public string ? h_user_deletedby { get; set; }
        public DateTime ? h_user_deletedat { get; set; }
    }

    public class user_details 
    {
        public string ? d_user_id { get; set; }
        public string? dh_user_id { get; set; }
        public string? d_user_name { get; set; }
        public string? d_avatarurl { get; set; }
        public string? d_bio { get; set; }
        public bool? d_is_deleted { get; set; }
        public string? d_user_createdby { get; set; }
        public DateTime d_user_createdat { get; set; }
        public string? d_user_modifiedby { get; set; }
        public DateTime? d_user_modifiedat { get; set; }
        public string? d_user_deletedby { get; set; }
        public DateTime? d_user_deletedat { get; set; }
    }

}
