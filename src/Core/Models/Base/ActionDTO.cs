namespace Core.Models.Base;

    public class ActionDto
    {

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public string Format { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
