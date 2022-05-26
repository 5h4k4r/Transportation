namespace Core.Models.Base;
    public class DepartmentRoleUserDto
    {
        public ulong Id { get; set; }
        public ulong RoleUserId { get; set; }
        public uint DepartmentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

}
