namespace Core.Models.Base;
    public class NoServantRequestDto
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
