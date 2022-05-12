using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class UserDTO
    {

        public ulong Id { get; set; }
        public string Mobile { get; set; } = null!;
        public string? Name { get; set; }
        public string? AuthId { get; set; }
        public byte? GenderId { get; set; }
        public uint? LanguageId { get; set; }
        public DateOnly? BirthDate { get; set; }
        public ulong? AreaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<RoleUserDTO>? RoleUsers { get; set; }
        public virtual LanguageDTO? Language { get; set; }

    }
}
