using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class VehicleDTO
    {

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public ulong? UsageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // public VehicleDetailDTO? VehicleDetail { get; set; }
        public virtual ICollection<VehicleDetailDTO>? VehicleDetails { get; set; }

    }
}
