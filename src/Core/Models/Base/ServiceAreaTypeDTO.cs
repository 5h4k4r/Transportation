namespace Core.Models.Base
{
    public class ServiceAreaTypeDto
    {

        public ulong Id { get; set; }
        public ulong ServiceId { get; set; }
        public string AreaId { get; set; } = null!;
        public ulong CategoryId { get; set; }
        public ulong? TypeId { get; set; }
        public ulong? UsageId { get; set; }
        public string Params { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public CategoryDto? Category { get; set; }

        public UsageDto Usage { get; set; } = null!;

    }
