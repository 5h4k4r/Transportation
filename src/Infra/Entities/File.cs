namespace Infra.Entities
{
    public sealed class File
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

        public ICollection<ClientFile> ClientFiles { get; set; }
        public ICollection<FileModel> FileModels { get; set; }
    }
}
