using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class ModelHasPermission
    {
        public ulong Id { get; set; }
        public uint PermissionId { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
