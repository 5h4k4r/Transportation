namespace Core.Models.Base
{
    public class OfferTemplateConditionDto
    {
        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public string Inputs { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
