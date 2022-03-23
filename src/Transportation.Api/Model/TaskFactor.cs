using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class TaskFactor
    {
        public ulong Id { get; set; }
        public ulong TaskId { get; set; }
        public string Data { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Task Task { get; set; } = null!;
    }
}
