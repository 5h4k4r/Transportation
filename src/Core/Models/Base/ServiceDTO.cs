namespace Core.Models.Base
{
    public class ServiceDto
    {

        public ulong Id { get; set; }
        public string Pin { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
