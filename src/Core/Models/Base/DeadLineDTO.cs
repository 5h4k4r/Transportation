using System;
using System.Collections.Generic;

namespace Core.Models;
    public partial class DeadLineDTO
    {
        public ulong Id { get; set; }
        public ulong RequestId { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
        public TimeOnly GoingTime { get; set; }
  
}
