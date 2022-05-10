using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Device
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Platform { get; set; } = null!;
        public string DeviceId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
