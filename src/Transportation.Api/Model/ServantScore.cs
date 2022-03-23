using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ServantScore
    {
        public ulong Id { get; set; }
        public ulong ServantId { get; set; }
        public ulong ServiceId { get; set; }
        public double Score { get; set; }
        public uint Number { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Servant Servant { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
