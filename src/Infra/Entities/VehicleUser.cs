using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class VehicleUser
    {
        public ulong Id { get; set; }
        public ulong VehicleId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
