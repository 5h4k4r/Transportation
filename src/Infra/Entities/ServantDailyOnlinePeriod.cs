using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class ServantDailyOnlinePeriod
    {
        public ulong Id { get; set; }
        public ulong ServantDailyStatisticId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public virtual ServantDailyStatistic ServantDailyStatistic { get; set; } = null!;
    }
}
