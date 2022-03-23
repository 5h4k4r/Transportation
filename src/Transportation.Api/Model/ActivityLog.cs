using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ActivityLog
    {
        public ulong Id { get; set; }
        public ulong ActionBy { get; set; }
        public string Description { get; set; } = null!;
        public string ActionToType { get; set; } = null!;
        public ulong ActionToId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Employee ActionByNavigation { get; set; } = null!;
    }
}
