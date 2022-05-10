using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class RequestRequirementDTO
    {
        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public ulong FieldId { get; set; }
        public string Value { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
