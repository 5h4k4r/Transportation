namespace Infra.Entities
{
    public class ServantDailyStatistic
    {
        public ulong Id { get; set; }
        public ulong DayId { get; set; }
        public ulong ServantId { get; set; }
        public ulong ServiceId { get; set; }
        public ushort DeliveredRequest { get; set; }
        public ushort RejectedRequest { get; set; }
        public byte SuccessTask { get; set; }
        public byte RejectedTask { get; set; }
        public uint OnlineDuration { get; set; }
        public uint DurationOnTask { get; set; }
        public uint DistanceOnTask { get; set; }
        public uint TasksAmount { get; set; }
        public uint TasksCommission { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ServantWorkDay? Day { get; set; } 
        public virtual Servant? Servant { get; set; } 
        public virtual Service? Service { get; set; } 
    }
}
