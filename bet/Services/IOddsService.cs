namespace FutebolSimplesBetsHub.Services
{
    public interface IOddsService
    {
        Task<(decimal home, decimal draw, decimal away, DateTime matchTime, string realTeams)> GetRealisticOddsAsync();
        Task<decimal> GetRandomOddsAsync(decimal min, decimal max);
    }
}
