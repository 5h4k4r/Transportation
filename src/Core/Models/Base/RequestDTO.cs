namespace Core.Models.Base
{
    public class RequestDto
    {
        public ulong Id { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public DateTime? ReserveTime { get; set; }
        public int KubakPrice { get; set; }
        public int UserPrice { get; set; }
        public string? Discount { get; set; }
        public string? Type { get; set; }
        public sbyte Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ServiceAreaTypeDto ServiceAreaType { get; set; } = null!;
    }
}
