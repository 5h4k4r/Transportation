using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Gender
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

        public virtual ICollection<GenderTranslation> GenderTranslations { get; set; }
        public virtual ICollection<Servant> Servants { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
