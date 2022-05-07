using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class TaskMessageDTO
    {
        public ulong TaskId { get; set; }
        public string? ChatId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

