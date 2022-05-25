namespace Infra.Entities
{
    public sealed class Service
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

        public ICollection<Account> Accounts { get; set; }
        public ICollection<DailyStatistic> DailyStatistics { get; set; }
        public ICollection<RequestOptionService> RequestOptionServices { get; set; }
        public ICollection<ServantDailyStatistic> ServantDailyStatistics { get; set; }
        public ICollection<ServantScore> ServantScores { get; set; }
        public ICollection<ServiceAreaType> ServiceAreaTypes { get; set; }
        public ICollection<ServiceTranslation> ServiceTranslations { get; set; }
        public ICollection<TaskHourlyStatistic> TaskHourlyStatistics { get; set; }
    }
}
