using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ServantScoreDTO
    {
        public ulong Id { get; set; }
        public ulong ServantId { get; set; }
        public ulong ServiceId { get; set; }
        public double Score { get; set; }
        public uint Number { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
