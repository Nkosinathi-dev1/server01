using SharedKernel.Entities;

namespace AuthService.Models
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;

        // Foreign key
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
