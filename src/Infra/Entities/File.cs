using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class File
    {
        public File()
        {
            ClientFiles = new HashSet<ClientFile>();
            FileModels = new HashSet<FileModel>();
        }

        public ulong Id { get; set; }
        public string Path { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<ClientFile> ClientFiles { get; set; }
        public virtual ICollection<FileModel> FileModels { get; set; }
    }
}
