using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class VehicleOwnerDTO
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong VehicleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
