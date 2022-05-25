namespace Infra.Entities
{
    public class AreaDepartment
    {
        public ulong Id { get; set; }
        public ulong? AreaId { get; set; }
        public uint? DepartmentId { get; set; }
        public ulong? RoleUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual AreaInfo? Area { get; set; }
        public virtual Department? Department { get; set; }
    }
}
