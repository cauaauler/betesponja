using FutebolSimplesBetsHub.Models;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Services
{
    public interface IBetService
    {
        Task<Bet> CreateBetAsync(BetViewModel betViewModel, int userId);
        Task<IEnumerable<Bet>> GetUserBetsAsync(int userId);
        Task<Bet?> GetBetByIdAsync(int id);
        Task<bool> UpdateBetStatusAsync(int betId, string status);
        Task<decimal> GetUserBalanceAsync(int userId);
        Task<bool> UpdateUserBalanceAsync(int userId, decimal amount);
    }
} 