namespace FutebolSimplesBetsHub.Models.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public string Participant1 { get; set; } = string.Empty;
        public string Participant2 { get; set; } = string.Empty;
        public string Competition { get; set; } = string.Empty;
        public string CompetitionId { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public decimal OddsParticipant1 { get; set; }
        public decimal OddsDraw { get; set; }
        public decimal OddsParticipant2 { get; set; }
        public bool IsLive { get; set; }
        
        public MatchOddsViewModel Odds => new()
        {
            Participant1 = OddsParticipant1,
            Draw = OddsDraw,
            Participant2 = OddsParticipant2
        };
    }
    
    public class MatchOddsViewModel
    {
        public decimal Participant1 { get; set; }
        public decimal Draw { get; set; }
        public decimal Participant2 { get; set; }
    }
} 