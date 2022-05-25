namespace Infra.Entities
{
    public sealed class Unit
    {
        public Unit()
        {
            Labels = new HashSet<Label>();
            UnitTranslations = new HashSet<UnitTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Label> Labels { get; set; }
        public ICollection<UnitTranslation> UnitTranslations { get; set; }
    }
}
