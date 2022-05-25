namespace Core.Models.Base
{
    public class UserTokenDto
    {
        public ulong UserId { get; set; }
        public string? Environment { get; set; }
        public string Shadow { get; set; } = null!;
        public DateTime ExpireAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
