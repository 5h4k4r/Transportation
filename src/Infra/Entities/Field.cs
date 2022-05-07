using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Field
    {
        public Field()
        {
            RequestRequirements = new HashSet<RequestRequirement>();
        }

        public ulong Id { get; set; }
        public ulong SegmentId { get; set; }
        public ulong LabelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Label Label { get; set; } = null!;
        public virtual Segment Segment { get; set; } = null!;
        public virtual ICollection<RequestRequirement> RequestRequirements { get; set; }
    }
}
