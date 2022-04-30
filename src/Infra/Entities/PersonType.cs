using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class PersonType
    {
        public PersonType()
        {
            PersonTypeTranslations = new HashSet<PersonTypeTranslation>();
        }

        public ulong Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<PersonTypeTranslation> PersonTypeTranslations { get; set; }
    }
}
