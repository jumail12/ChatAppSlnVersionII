using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSlnVersionII.Domain.Entities.Module
{
    public class ModuleDocs
    {
        public string docId { get; set; } 
        public string docMod { get; set; } = string.Empty;          // Module Name (e.g., "User", "Auth")
        public string docPrefix { get; set; } = string.Empty;       // Prefix for IDs (e.g., "US")
        public string? docDescription { get; set; }                 // Optional description

        public int docStartNumber { get; set; } = 1;                // Starting number for ID generation
        public int docLength { get; set; } = 5;                     // Number of digits (e.g., 5 -> 00001)
        public int docCurrentNumber { get; set; } = 0;              // Last used number tracker

        public bool docIsDeleted { get; set; } = false;

        public string? docCreatedBy { get; set; }
        public string? docModifiedBy { get; set; }
        public string? docDeletedBy { get; set; }

        public DateTime docCreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? docModifiedAt { get; set; }
        public DateTime? docDeletedAt { get; set; }
    }
}
