using PrimumCore.Models.Enums;

namespace PrimumCore.Models
{
    public class VerificationToken
    {
        public int TokenId { get; set; }

        public string Token {  get; set; }

        public DateTime LifeTime { get; set; }

        public TokenMeaning Meaning { get; set; }

        public bool IsUsed { get; set; } = false;

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
