using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class NoServantRequest
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ServiceAreaType ServiceAreaType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
