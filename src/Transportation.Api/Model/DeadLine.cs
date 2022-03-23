using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class DeadLine
    {
        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
        public TimeOnly GoingTime { get; set; }
        public TimeOnly ReturnTime { get; set; }

        public virtual Request Request { get; set; } = null!;
    }
}
