namespace Infra.Entities
{
    public sealed class Gender
    {
        public Gender()
        {
            GenderTranslations = new HashSet<GenderTranslation>();
            Servants = new HashSet<Servant>();
            Users = new HashSet<User>();
        }

        public byte Id { get; set; }
        public string? Key { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<GenderTranslation> GenderTranslations { get; set; }
        public ICollection<Servant> Servants { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
