using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Domain.Entities.Roles
{
    public class RoleGroup
    {
        public string? role_id {  get; set; }
        public string? role_type { get; set; }
        public string? role_description { get; set; }
        public bool? is_deleted { get; set; }

        public string? role_CreatedBy { get; set; }
        public string? role_ModifiedBy { get; set; }
        public string? role_DeletedBy { get; set; }

        public DateTime role_CreatedAt { get; set; } 
        public DateTime? role_ModifiedAt { get; set; }
        public DateTime? role_DeletedAt { get; set; }
    }

    public class role_mapping
    {
        public string? map_id { get; set; }
        public string? map_userid { get; set; }
        public string? map_role_grpid { get; set; }
        public bool? map_isdeleted { get; set; }

        public string? map_createdby { get; set; }
        public string? map_modifiedby { get; set; }
        public string? map_deletedby { get; set; }

        public DateTime map_createdat { get; set; }
        public DateTime? map_modifiedat { get; set; }
        public DateTime? map_deletedat { get; set; }
    }
}
