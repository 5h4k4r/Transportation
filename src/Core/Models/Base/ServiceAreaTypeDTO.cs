using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ServiceAreaTypeDTO
    {

        public ulong Id { get; set; }
        public ulong ServiceId { get; set; }
        public string AreaId { get; set; } = null!;
        public ulong CategoryId { get; set; }
        public ulong? TypeId { get; set; }
        public ulong? UsageId { get; set; }
        public string Params { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
