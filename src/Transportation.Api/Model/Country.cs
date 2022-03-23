using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public uint Id { get; set; }
        public string Title { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
