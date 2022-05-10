using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Requirement
    {
        public Requirement()
        {
            Segments = new HashSet<Segment>();
        }

        public uint Id { get; set; }
        public string Type { get; set; } = null!;
        public string ShowIn { get; set; } = null!;
        public ulong ServiceAreaTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
        public virtual ICollection<Segment> Segments { get; set; }
    }
}
