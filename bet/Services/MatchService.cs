using Microsoft.EntityFrameworkCore;
using FutebolSimplesBetsHub.Data;
using FutebolSimplesBetsHub.Models;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;

        public MatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MatchViewModel>> GetLiveMatchesAsync()
        {
            var matches = await _context.Matches
                .Where(m => m.IsLive)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return matches.Select(MapToViewModel);
        }

        public async Task<IEnumerable<MatchViewModel>> GetUpcomingMatchesAsync()
        {
            var matches = await _context.Matches
                .Where(m => !m.IsLive && m.MatchDate > DateTime.Now)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return matches.Select(MapToViewModel);
        }

        public async Task<IEnumerable<MatchViewModel>> GetAllMatchesAsync()
        {
            var matches = await _context.Matches
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return matches.Select(MapToViewModel);
        }

        public async Task<IEnumerable<MatchViewModel>> GetMatchesByCompetitionAsync(string competitionId)
        {
            var matches = await _context.Matches
                .Where(m => m.CompetitionId == competitionId)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return matches.Select(MapToViewModel);
        }

        public async Task<IEnumerable<MatchViewModel>> SearchMatchesAsync(string searchTerm)
        {
            var matches = await _context.Matches
                .Where(m => m.Participant1.Contains(searchTerm) || 
                           m.Participant2.Contains(searchTerm) || 
                           m.Competition.Contains(searchTerm))
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return matches.Select(MapToViewModel);
        }

        public async Task<Match?> GetMatchByIdAsync(int id)
        {
            return await _context.Matches.FindAsync(id);
        }

        public async Task<IEnumerable<string>> GetCompetitionsAsync()
        {
            return await _context.Matches
                .Select(m => m.Competition)
                .Distinct()
                .ToListAsync();
        }

        private static MatchViewModel MapToViewModel(Match match)
        {
            return new MatchViewModel
            {
                Id = match.Id,
                Participant1 = match.Participant1,
                Participant2 = match.Participant2,
                Competition = match.Competition,
                CompetitionId = match.CompetitionId,
                Date = match.Date,
                Time = match.Time,
                OddsParticipant1 = match.OddsParticipant1,
                OddsDraw = match.OddsDraw,
                OddsParticipant2 = match.OddsParticipant2,
                IsLive = match.IsLive
            };
        }
    }
} 