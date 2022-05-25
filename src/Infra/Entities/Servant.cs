namespace Infra.Entities
{
    public sealed class Servant
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
        public string? Address { get; set; } 
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Gender? Gender { get; set; }
        public User User { get; set; } = null!;
        public ICollection<ServantDailyStatistic> ServantDailyStatistics { get; set; }
        public ICollection<ServantScore> ServantScores { get; set; }
        public ICollection<ServantStatus> ServantStatuses { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
