using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Service
    {
        public Service()
        {
            Accounts = new HashSet<Account>();
            DailyStatistics = new HashSet<DailyStatistic>();
            RequestOptionServices = new HashSet<RequestOptionService>();
            ServantDailyStatistics = new HashSet<ServantDailyStatistic>();
            ServantScores = new HashSet<ServantScore>();
            ServiceAreaTypes = new HashSet<ServiceAreaType>();
            ServiceTranslations = new HashSet<ServiceTranslation>();
            TaskHourlyStatistics = new HashSet<TaskHourlyStatistic>();
        }

        public ulong Id { get; set; }
        public string Pin { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<DailyStatistic> DailyStatistics { get; set; }
        public virtual ICollection<RequestOptionService> RequestOptionServices { get; set; }
        public virtual ICollection<ServantDailyStatistic> ServantDailyStatistics { get; set; }
        public virtual ICollection<ServantScore> ServantScores { get; set; }
        public virtual ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
        public virtual ICollection<ServiceTranslation> ServiceTranslations { get; set; }
        public virtual ICollection<TaskHourlyStatistic> TaskHourlyStatistics { get; set; }
    }
}
