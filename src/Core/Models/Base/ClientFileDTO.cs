namespace Core.Models.Base;
    public class ClientFileDto
    {
        public ulong Id { get; set; }
        public ulong FileId { get; set; }
        public uint LanguageId { get; set; }
        public string Platform { get; set; } = null!;
        public string? Version { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
