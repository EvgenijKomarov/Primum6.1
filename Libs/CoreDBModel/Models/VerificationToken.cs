using CoreDBModel.Models.Enums;

namespace CoreDBModel.Models
{
    public class VerificationToken: BaseEntity
    {
        public string Token {  get; set; }

        public DateTime LifeTime { get; set; }

        public TokenMeaning Meaning { get; set; }

        public bool IsUsed { get; set; } = false;

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}
