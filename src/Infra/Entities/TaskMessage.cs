using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class TaskMessage
    {
        public ulong TaskId { get; set; }
        public string? ChatId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
