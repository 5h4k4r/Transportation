namespace Core.Models.Base
{
    public class OptionServiceAreaTypeDto
    {
        public ulong Id { get; set; }
        public ulong OptionId { get; set; }
        public ulong ServiceAreaTypeId { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
