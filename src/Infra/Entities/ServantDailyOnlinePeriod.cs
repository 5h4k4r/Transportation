namespace Infra.Entities
{
    public class ServantDailyOnlinePeriod
    {
        public ulong Id { get; set; }
        public ulong ServantDailyStatisticId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public virtual ServantDailyStatistic ServantDailyStatistic { get; set; } = null!;
    }
}
