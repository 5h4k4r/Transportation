using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Location
    {
        public ulong Id { get; set; }
        public ulong TraceId { get; set; }
        public string? Points { get; set; }
        public string? ModifiedPoints { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Trace Trace { get; set; } = null!;
    }
}
