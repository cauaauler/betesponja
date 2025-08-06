namespace FutebolSimplesBetsHub.Models.ViewModels
{
    public class BetViewModel
    {
        public int MatchId { get; set; }
        public string Participant1 { get; set; } = string.Empty;
        public string Participant2 { get; set; } = string.Empty;
        public string Competition { get; set; } = string.Empty;
        public string BetType { get; set; } = string.Empty;
        public decimal Odds { get; set; }
        public decimal Amount { get; set; }
        public decimal PotentialWin { get; set; }
    }
    
    public class BetConfirmationViewModel
    {
        public int MatchId { get; set; }
        public string Participant1 { get; set; } = string.Empty;
        public string Participant2 { get; set; } = string.Empty;
        public string BetType { get; set; } = string.Empty;
        public decimal Odds { get; set; }
        public decimal Amount { get; set; }
        public decimal PotentialWin { get; set; }
        public string Competition { get; set; } = string.Empty;
    }
} 