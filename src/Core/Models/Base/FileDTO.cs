namespace Core.Models.Base;
    public class FileDto
    {
       

        public ulong Id { get; set; }
        public string Path { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
