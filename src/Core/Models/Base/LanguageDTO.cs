namespace Core.Models.Base;
    public class LanguageDto
    {

        public uint Id { get; set; }
        public string Title { get; set; } = null!;
        public string Locale { get; set; } = null!;
        public string Direction { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
