using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class RequirementDTO
    {

        public uint Id { get; set; }
        public string Type { get; set; } = null!;
        public string ShowIn { get; set; } = null!;
        public ulong ServiceAreaTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
