namespace Core.Models.Base;
    public class CommissionDto
    {
        public ulong Id { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public double Value { get; set; }
        public bool IsWithdrawFromGift { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
