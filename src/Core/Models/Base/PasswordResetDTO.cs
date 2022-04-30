using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class PasswordResetDTO
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }
}
