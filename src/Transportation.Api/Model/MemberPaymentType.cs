using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class MemberPaymentType
    {
        public ulong Id { get; set; }
        public ulong MemberId { get; set; }
        public ulong TaskId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
    }
}
