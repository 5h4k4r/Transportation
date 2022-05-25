namespace Core.Models.Base
{
    public class TaxiMeterDto
    {
        public ulong TaskId { get; set; }
        public string Point1 { get; set; } = null!;
        public string? Point2 { get; set; }
        public string LastPoint { get; set; } = null!;
        public ulong Time1 { get; set; }
        public ulong Time2 { get; set; }
        public uint Amount { get; set; }
        public uint Duration { get; set; }
        public uint Distance { get; set; }
    }
}
