using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class PermissionDTO
    {
        public ulong Id { get; set; }
        public string? Title { get; set; }
        public string Slug { get; set; } = null!;
        /// <summary>
        /// permission_id
        /// </summary>
        public ulong? Dependency { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
