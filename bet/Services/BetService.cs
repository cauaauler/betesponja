using Microsoft.EntityFrameworkCore;
using FutebolSimplesBetsHub.Data;
using FutebolSimplesBetsHub.Models;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Services
{
    public class BetService : IBetService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMatchService _matchService;

        public BetService(ApplicationDbContext context, IMatchService matchService)
        {
            _context = context;
            _matchService = matchService;
        }

        public async Task<Bet> CreateBetAsync(BetViewModel betViewModel, int userId)
        {
            var match = await _matchService.GetMatchByIdAsync(betViewModel.MatchId);
            if (match == null)
                throw new ArgumentException("Partida não encontrada");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new ArgumentException("Usuário não encontrado");

            if (user.Balance < betViewModel.Amount)
                throw new InvalidOperationException("Saldo insuficiente");

            // Deduct amount from user balance
            user.Balance -= betViewModel.Amount;
            await _context.SaveChangesAsync();

            var bet = new Bet
            {
                MatchId = betViewModel.MatchId,
                UserId = userId,
                BetType = betViewModel.BetType,
                Amount = betViewModel.Amount,
                Odds = betViewModel.Odds,
                PotentialWin = betViewModel.Amount * betViewModel.Odds,
                Status = "pending",
                CreatedAt = DateTime.Now
            };

            _context.Bets.Add(bet);
            await _context.SaveChangesAsync();

            return bet;
        }

        public async Task<IEnumerable<Bet>> GetUserBetsAsync(int userId)
        {
            return await _context.Bets
                .Include(b => b.Match)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Bet?> GetBetByIdAsync(int id)
        {
            return await _context.Bets
                .Include(b => b.Match)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> UpdateBetStatusAsync(int betId, string status)
        {
            var bet = await _context.Bets
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == betId);

            if (bet == null)
                return false;

            bet.Status = status;
            bet.SettledAt = DateTime.Now;

            // If bet is won, add winnings to user balance
            if (status == "won")
            {
                bet.User.Balance += bet.PotentialWin;
            }
            // If bet is lost, amount is already deducted
            // If bet is cancelled, return the original amount
            else if (status == "cancelled")
            {
                bet.User.Balance += bet.Amount;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetUserBalanceAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.Balance ?? 0;
        }

        public async Task<bool> UpdateUserBalanceAsync(int userId, decimal amount)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.Balance += amount;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 