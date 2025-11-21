using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Dtos.Previlage
{
    public class PrevilageGroupDto
    {
        public string? prg_id { get; set; }
        public string? prg_group { get; set; }
        public string? prg_description { get; set; }
    }

    public class PrevilageDto
    {
        public string? prv_id { get; set; }
        public string? prv_prgid { get; set; }
        public string? prv_privilegename { get; set; }
        public string? prv_description { get; set; }
        public string? prg_group { get; set; }
    }
}
