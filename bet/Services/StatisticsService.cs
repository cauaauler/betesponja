using Microsoft.EntityFrameworkCore;
using FutebolSimplesBetsHub.Data;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StatisticsViewModel> GetHomePageStatisticsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var weekStart = today.AddDays(-(int)today.DayOfWeek);
            var weekEnd = weekStart.AddDays(7);

            // Get today's matches count
            var todayMatchesCount = await _context.Matches
                .Where(m => m.MatchDate >= today && m.MatchDate < tomorrow)
                .CountAsync();

            // Get live matches count
            var liveMatchesCount = await _context.Matches
                .Where(m => m.IsLive)
                .CountAsync();

            // Get upcoming matches count
            var upcomingMatchesCount = await _context.Matches
                .Where(m => !m.IsLive && m.MatchDate > DateTime.Now)
                .CountAsync();

            // Get active bets count (pending bets)
            var activeBetsCount = await _context.Bets
                .Where(b => b.Status == "pending")
                .CountAsync();

            // Get total amount in active bets
            var totalBetsAmount = await _context.Bets
                .Where(b => b.Status == "pending")
                .SumAsync(b => b.Amount);

            // Get average bet amount
            var pendingBets = await _context.Bets
                .Where(b => b.Status == "pending")
                .ToListAsync();
            var averageBetAmount = pendingBets.Any() ? pendingBets.Average(b => b.Amount) : 0;

            // Get highest odds today
            var todayMatches = await _context.Matches
                .Where(m => m.MatchDate >= today && m.MatchDate < tomorrow)
                .ToListAsync();
            
            var highestOddsToday = todayMatches.Any() 
                ? todayMatches.Max(m => Math.Max(Math.Max(m.OddsParticipant1, m.OddsDraw), m.OddsParticipant2))
                : 0;

            // Get total users count
            var totalUsers = await _context.Users
                .Where(u => u.IsActive)
                .CountAsync();

            // Get total winnings today (settled bets that were won)
            var totalWinningsToday = await _context.Bets
                .Where(b => b.Status == "won" && b.SettledAt >= today && b.SettledAt < tomorrow)
                .SumAsync(b => b.PotentialWin);

            // Get total matches this week
            var totalMatchesThisWeek = await _context.Matches
                .Where(m => m.MatchDate >= weekStart && m.MatchDate < weekEnd)
                .CountAsync();

            return new StatisticsViewModel
            {
                TodayMatchesCount = todayMatchesCount,
                LiveMatchesCount = liveMatchesCount,
                UpcomingMatchesCount = upcomingMatchesCount,
                ActiveBetsCount = activeBetsCount,
                TotalBetsAmount = totalBetsAmount,
                AverageBetAmount = averageBetAmount,
                HighestOddsToday = highestOddsToday,
                TotalUsers = totalUsers,
                TotalWinningsToday = totalWinningsToday,
                TotalMatchesThisWeek = totalMatchesThisWeek
            };
        }
    }
}
