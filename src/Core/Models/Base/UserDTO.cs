namespace Core.Models.Base
{
    public class UserDto
    {

        public ulong Id { get; set; }
        public string Mobile { get; set; }
        public string? Name { get; set; }
        public string? AuthId { get; set; }
        public byte? GenderId { get; set; }
        public uint? LanguageId { get; set; }
        public DateOnly? BirthDate { get; set; }
        public ulong? AreaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<RoleUserDto> RoleUsers { get; set; } = null!;
        public virtual LanguageDto? Language { get; set; }
        public virtual AreaInfoDto? AreaInfo { get; set; }

    }
}
