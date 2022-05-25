namespace Core.Models.Base
{
    public class ServantScoreDto
    {
        public ulong Id { get; set; }
        public ulong ServantId { get; set; }
        public ulong ServiceId { get; set; }
        public double Score { get; set; }
        public uint Number { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
