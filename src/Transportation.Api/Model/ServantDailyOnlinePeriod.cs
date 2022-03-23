using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class ServantDailyOnlinePeriod
    {
        public ulong Id { get; set; }
        public ulong ServantDailyStatisticId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
