using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class TaskMessage
    {
        public ulong TaskId { get; set; }
        public string? ChatId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
