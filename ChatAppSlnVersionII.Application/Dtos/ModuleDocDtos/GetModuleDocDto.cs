using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Application.Dtos.ModuleDocDtos
{
    public class GetModuleDocDto
    {
        public Guid docId { get; set; }
        public string docMod { get; set; } = string.Empty;    
        public string docPrefix { get; set; } = string.Empty; 
        public string? docDescription { get; set; }           

        public int docStartNumber { get; set; } = 1;          
        public int docLength { get; set; } = 5;               
        public int docCurrentNumber { get; set; } = 0;
    }
}
