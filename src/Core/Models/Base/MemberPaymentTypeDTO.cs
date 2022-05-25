namespace Core.Models.Base;
    public class MemberPaymentTypeDto
    {
        public ulong Id { get; set; }
        public ulong MemberId { get; set; }
        public ulong TaskId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
