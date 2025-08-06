using System.ComponentModel.DataAnnotations;

namespace FutebolSimplesBetsHub.Models
{
    public class Match
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Participant1 { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Participant2 { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Competition { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string CompetitionId { get; set; } = string.Empty;
        
        public DateTime MatchDate { get; set; }
        
        [StringLength(10)]
        public string Date { get; set; } = string.Empty;
        
        [StringLength(10)]
        public string Time { get; set; } = string.Empty;
        
        public decimal OddsParticipant1 { get; set; }
        public decimal OddsDraw { get; set; }
        public decimal OddsParticipant2 { get; set; }
        
        public bool IsLive { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public virtual ICollection<Bet> Bets { get; set; } = new List<Bet>();
    }
} 