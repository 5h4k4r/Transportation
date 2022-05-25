namespace Infra.Entities
{
    public sealed class Department
    {
        public Department()
        {
            AreaDepartments = new HashSet<AreaDepartment>();
            DepartmentRoleUsers = new HashSet<DepartmentRoleUser>();
        }

        public uint Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<AreaDepartment> AreaDepartments { get; set; }
        public ICollection<DepartmentRoleUser> DepartmentRoleUsers { get; set; }
    }
}
