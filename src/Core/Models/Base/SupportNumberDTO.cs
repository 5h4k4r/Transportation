namespace Core.Models.Base
{
    public class SupportNumberDto
    {
        public ulong Id { get; set; }
        public ulong AreaId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
