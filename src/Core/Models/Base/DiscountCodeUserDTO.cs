namespace Core.Models.Base;
    public class DiscountCodeUserDto
    {
        public ulong Id { get; set; }
        public ulong DiscountCodeId { get; set; }
        public ulong UserId { get; set; }
        
        public int Amount { get; set; }

        public int count { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public bool Used { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public DiscountCodeDto? DiscountCode { get; set; }

}
