using FutebolSimplesBetsHub.Models;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Services
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchViewModel>> GetLiveMatchesAsync();
        Task<IEnumerable<MatchViewModel>> GetUpcomingMatchesAsync();
        Task<IEnumerable<MatchViewModel>> GetAllMatchesAsync();
        Task<IEnumerable<MatchViewModel>> GetMatchesByCompetitionAsync(string competitionId);
        Task<IEnumerable<MatchViewModel>> SearchMatchesAsync(string searchTerm);
        Task<Match?> GetMatchByIdAsync(int id);
        Task<IEnumerable<string>> GetCompetitionsAsync();
    }
} 