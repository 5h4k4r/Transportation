using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class RounderDTO
    {
        public ushort Id { get; set; }
        public string Currency { get; set; } = null!;
        public double Divisor { get; set; }
        public string Method { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
