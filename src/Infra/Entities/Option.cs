namespace Infra.Entities
{
    public sealed class Option
    {
        public Option()
        {
            OptionServiceAreaTypes = new HashSet<OptionServiceAreaType>();
            OptionSubscribers = new HashSet<OptionSubscriber>();
            RequestOptionServices = new HashSet<RequestOptionService>();
        }

        public ulong Id { get; set; }
        public string Type { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<OptionServiceAreaType> OptionServiceAreaTypes { get; set; }
        public ICollection<OptionSubscriber> OptionSubscribers { get; set; }
        public ICollection<RequestOptionService> RequestOptionServices { get; set; }
    }
}
