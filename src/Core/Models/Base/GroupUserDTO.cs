namespace Core.Models.Base;
    public class GroupUserDto
    {
        public ulong Id { get; set; }
        public ulong GroupId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
