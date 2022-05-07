using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class CanceledTask
    {
        public ulong Id { get; set; }
        public ulong TaskId { get; set; }
        public ulong? CancelReasonId { get; set; }
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual CancelReason? CancelReason { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
