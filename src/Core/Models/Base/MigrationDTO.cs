namespace Core.Models.Base
{
    public class MigrationDto
    {
        public uint Id { get; set; }
        public string Migration1 { get; set; } = null!;
        public int Batch { get; set; }
    }
}
