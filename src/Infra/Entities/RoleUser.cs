namespace Infra.Entities
{
    public sealed class RoleUser
    {
        public RoleUser()
        {
            DepartmentRoleUsers = new HashSet<DepartmentRoleUser>();
        }

        public ulong Id { get; set; }
        public byte RoleId { get; set; }
        public ulong UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Role Role { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<DepartmentRoleUser> DepartmentRoleUsers { get; set; }
    }
}
