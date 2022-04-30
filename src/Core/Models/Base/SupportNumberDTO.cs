using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class SupportNumberDTO
    {
        public ulong Id { get; set; }
        public ulong AreaId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
