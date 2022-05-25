namespace Core.Models.Base;
    public class ActiveRoleDto
    {
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
