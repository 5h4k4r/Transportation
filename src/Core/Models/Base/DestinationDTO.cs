namespace Core.Models.Base;
    public class DestinationDto
    {
        public ulong Id { get; set; }
        public string ModelType { get; set; } = null!;
        public ulong ModelId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Price { get; set; }
        public uint Distance { get; set; }
        public uint Duration { get; set; }
        public sbyte Status { get; set; }
        public sbyte Step { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

