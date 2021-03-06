using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ServantStatusDTO
    {
        public ulong Id { get; set; }
        public ulong ServantId { get; set; }
        public ulong? ServiceId { get; set; }
        public ulong? AreaId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Status { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
