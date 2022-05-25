namespace Infra.Entities
{
    public sealed class PersonType
    {
        public PersonType()
        {
            PersonTypeTranslations = new HashSet<PersonTypeTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<PersonTypeTranslation> PersonTypeTranslations { get; set; }
    }
}
