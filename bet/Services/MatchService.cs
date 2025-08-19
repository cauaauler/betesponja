using Microsoft.EntityFrameworkCore;
using FutebolSimplesBetsHub.Data;
using FutebolSimplesBetsHub.Models;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOddsService _oddsService;

        public MatchService(ApplicationDbContext context, IOddsService oddsService)
        {
            _context = context;
            _oddsService = oddsService;
        }

        public async Task<IEnumerable<MatchViewModel>> GetLiveMatchesAsync()
        {
            var matches = await _context.Matches
                .Where(m => m.IsLive)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            var result = new List<MatchViewModel>();

            // Atualizar odds e horários com dados realistas da API
            foreach (var match in matches)
            {
                var (home, draw, away, matchTime, realTeams) = await _oddsService.GetRealisticOddsAsync();
                match.OddsParticipant1 = home;
                match.OddsDraw = draw;
                match.OddsParticipant2 = away;
                match.MatchDate = matchTime;
                
                var viewModel = MapToViewModel(match);
                if (!string.IsNullOrWhiteSpace(realTeams) && realTeams.Contains(" vs "))
                {
                    var parts = realTeams.Split(" vs ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        viewModel.Participant1 = parts[0];
                        viewModel.Participant2 = parts[1];
                    }
                }
                result.Add(viewModel);
            }

            return result;
        }

        public async Task<IEnumerable<MatchViewModel>> GetUpcomingMatchesAsync()
        {
            var matches = await _context.Matches
                .Where(m => !m.IsLive && m.MatchDate > DateTime.Now)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            var result = new List<MatchViewModel>();

            // Atualizar odds e horários com dados realistas da API
            foreach (var match in matches)
            {
                var (home, draw, away, matchTime, realTeams) = await _oddsService.GetRealisticOddsAsync();
                match.OddsParticipant1 = home;
                match.OddsDraw = draw;
                match.OddsParticipant2 = away;
                match.MatchDate = matchTime;
                
                var viewModel = MapToViewModel(match);
                if (!string.IsNullOrWhiteSpace(realTeams) && realTeams.Contains(" vs "))
                {
                    var parts = realTeams.Split(" vs ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        viewModel.Participant1 = parts[0];
                        viewModel.Participant2 = parts[1];
                    }
                }
                result.Add(viewModel);
            }

            return result;
        }

        public async Task<IEnumerable<MatchViewModel>> GetAllMatchesAsync()
        {
            var matches = await _context.Matches
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            var result = new List<MatchViewModel>();

            // Atualizar odds e horários com dados realistas da API
            foreach (var match in matches)
            {
                var (home, draw, away, matchTime, realTeams) = await _oddsService.GetRealisticOddsAsync();
                match.OddsParticipant1 = home;
                match.OddsDraw = draw;
                match.OddsParticipant2 = away;
                match.MatchDate = matchTime;
                
                var viewModel = MapToViewModel(match);
                if (!string.IsNullOrWhiteSpace(realTeams) && realTeams.Contains(" vs "))
                {
                    var parts = realTeams.Split(" vs ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        viewModel.Participant1 = parts[0];
                        viewModel.Participant2 = parts[1];
                    }
                }
                result.Add(viewModel);
            }

            return result;
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
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                // Atualizar odds e horários com dados realistas da API
                var (home, draw, away, matchTime, realTeams) = await _oddsService.GetRealisticOddsAsync();
                match.OddsParticipant1 = home;
                match.OddsDraw = draw;
                match.OddsParticipant2 = away;
                match.MatchDate = matchTime;
            }
            return match;
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