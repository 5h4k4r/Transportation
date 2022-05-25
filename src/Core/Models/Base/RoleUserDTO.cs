namespace Core.Models.Base
{
    public class RoleUserDto
    {

        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
