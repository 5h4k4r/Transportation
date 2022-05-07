using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class CommissionDTO
    {
        public ulong Id { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public double Value { get; set; }
        public bool IsWithdrawFromGift { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
