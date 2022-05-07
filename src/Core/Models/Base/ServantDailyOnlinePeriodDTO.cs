using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ServantDailyOnlinePeriodDTO
    {
        public ulong Id { get; set; }
        public ulong ServantDailyStatisticId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
