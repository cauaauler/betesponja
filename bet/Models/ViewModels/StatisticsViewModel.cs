namespace FutebolSimplesBetsHub.Models.ViewModels
{
    public class StatisticsViewModel
    {
        public int TodayMatchesCount { get; set; }
        public int ActiveBetsCount { get; set; }
        public decimal TotalBetsAmount { get; set; }
        public decimal HighestOddsToday { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalWinningsToday { get; set; }
        public int LiveMatchesCount { get; set; }
        public int UpcomingMatchesCount { get; set; }
        public decimal AverageBetAmount { get; set; }
        public int TotalMatchesThisWeek { get; set; }
    }
}
