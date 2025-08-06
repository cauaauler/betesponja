using System.ComponentModel.DataAnnotations;

namespace FutebolSimplesBetsHub.Models
{
    public class Bet
    {
        public int Id { get; set; }
        
        public int MatchId { get; set; }
        public virtual Match Match { get; set; } = null!;
        
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        
        [Required]
        [StringLength(20)]
        public string BetType { get; set; } = string.Empty; // participant1, draw, participant2
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor da aposta deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        public decimal Odds { get; set; }
        
        public decimal PotentialWin { get; set; }
        
        public string Status { get; set; } = "pending"; // pending, won, lost, cancelled
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? SettledAt { get; set; }
    }
} 