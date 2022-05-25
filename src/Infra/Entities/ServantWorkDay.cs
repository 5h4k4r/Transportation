namespace Infra.Entities
{
    public sealed class ServantWorkDay
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

        public ICollection<DailyStatistic> DailyStatistics { get; set; }
        public ICollection<ServantDailyStatistic> ServantDailyStatistics { get; set; }
        public ICollection<TaskHourlyStatistic> TaskHourlyStatistics { get; set; }
    }
}
