namespace Core.Models.Base;
    public class FileModelDto
    {
        public ulong Id { get; set; }
        public string? Version { get; set; }
        public ulong FileId { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
