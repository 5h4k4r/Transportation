using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class ServantWorkDay
    {
        public ServantWorkDay()
        {
            DailyStatistics = new HashSet<DailyStatistic>();
            ServantDailyStatistics = new HashSet<ServantDailyStatistic>();
            TaskHourlyStatistics = new HashSet<TaskHourlyStatistic>();
        }

        public ulong Id { get; set; }
        public DateOnly Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<DailyStatistic> DailyStatistics { get; set; }
        public virtual ICollection<ServantDailyStatistic> ServantDailyStatistics { get; set; }
        public virtual ICollection<TaskHourlyStatistic> TaskHourlyStatistics { get; set; }
    }
}
