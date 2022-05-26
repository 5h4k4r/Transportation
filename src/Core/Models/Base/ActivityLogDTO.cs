namespace Core.Models.Base;

    public class ActivityLogDto
    {
        public ulong Id { get; set; }
        public ulong ActionBy { get; set; }
        public string Description { get; set; } = null!;
        public string ActionToType { get; set; } = null!;
        public ulong ActionToId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

