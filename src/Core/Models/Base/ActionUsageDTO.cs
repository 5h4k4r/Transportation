namespace Core.Models.Base;

    public class ActionUsageDto
    {
        public ulong ActionId { get; set; }
        public ulong UsageId { get; set; }
        public string Value { get; set; } = null!;

}
