namespace Core.Models.Base;
    public class FieldDto
    {
     
        public ulong Id { get; set; }
        public ulong SegmentId { get; set; }
        public ulong LabelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    
}
