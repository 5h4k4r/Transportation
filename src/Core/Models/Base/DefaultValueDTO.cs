namespace Core.Models.Base;
    public class DefaultValueDto
    {
        public ulong Id { get; set; }
        public uint LanguageId { get; set; }
        public string Values { get; set; } = null!;
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
