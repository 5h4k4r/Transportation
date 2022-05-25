namespace Infra.Entities
{
    public class UserArea
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public byte RoleId { get; set; }
        public ulong AreaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual AreaInfo Area { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
