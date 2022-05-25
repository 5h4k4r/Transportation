namespace Infra.Entities
{
    public class DepartmentRoleUser
    {
        public ulong Id { get; set; }
        public ulong RoleUserId { get; set; }
        public uint DepartmentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual RoleUser RoleUser { get; set; } = null!;
    }
}
