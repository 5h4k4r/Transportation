namespace Core.Models.Base;
    public class LabelDto
    {

        public ulong Id { get; set; }
        public ulong? UnitId { get; set; }
        public string? StaticKey { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
