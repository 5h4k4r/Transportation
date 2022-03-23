using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            VehicleDetails = new HashSet<VehicleDetail>();
            VehicleOwners = new HashSet<VehicleOwner>();
            VehicleUsers = new HashSet<VehicleUser>();
        }

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public ulong? UsageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<VehicleDetail> VehicleDetails { get; set; }
        public virtual ICollection<VehicleOwner> VehicleOwners { get; set; }
        public virtual ICollection<VehicleUser> VehicleUsers { get; set; }
    }
}
