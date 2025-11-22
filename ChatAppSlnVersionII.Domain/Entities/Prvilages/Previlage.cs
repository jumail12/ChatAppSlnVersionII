using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Domain.Entities.Prvilages
{
    public class role_previlage_mapping
    {
        public string rpm_id { get; set; }
        public string rpm_role_id { get; set; }
        public string rpm_privilage_id { get; set; }

        public bool rpm_is_granted { get; set; }
        public bool rpm_is_deleted { get; set; }

        public string rpm_created_by { get; set; }
        public string rpm_modified_by { get; set; }
        public string rpm_deleted_by { get; set; }

        public DateTime? rpm_created_at { get; set; }
        public DateTime? rpm_modified_at { get; set; }
        public DateTime? rpm_deleted_at { get; set; }
    }

}
