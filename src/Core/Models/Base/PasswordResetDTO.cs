namespace Core.Models.Base
{
    public class PasswordResetDto
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
