using System;
using System.Collections.Generic;

namespace Infra.Entities
{
    public partial class Servant
    {
        public Servant()
        {
            ServantDailyStatistics = new HashSet<ServantDailyStatistic>();
            ServantScores = new HashSet<ServantScore>();
            ServantStatuses = new HashSet<ServantStatus>();
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public ulong UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public string? Certificate { get; set; }
        public string? BankId { get; set; }
        public uint AreaId { get; set; }
        public byte? GenderId { get; set; }
        public string Address { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Gender? Gender { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<ServantDailyStatistic> ServantDailyStatistics { get; set; }
        public virtual ICollection<ServantScore> ServantScores { get; set; }
        public virtual ICollection<ServantStatus> ServantStatuses { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
