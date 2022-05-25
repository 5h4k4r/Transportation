namespace Core.Models.Base;
    public class AttributeDto
    {
       

        public ulong Id { get; set; }
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

 
}
