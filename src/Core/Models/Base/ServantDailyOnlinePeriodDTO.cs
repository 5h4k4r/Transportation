namespace Core.Models.Base
{
    public class ServantDailyOnlinePeriodDto
    {
        public ulong Id { get; set; }
        public ulong ServantDailyStatisticId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
