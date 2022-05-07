using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class MemberPaymentTypeDTO
    {
        public ulong Id { get; set; }
        public ulong MemberId { get; set; }
        public ulong TaskId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
