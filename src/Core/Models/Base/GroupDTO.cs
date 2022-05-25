namespace Core.Models.Base;
    public class GroupDto
    {

        public ulong Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
