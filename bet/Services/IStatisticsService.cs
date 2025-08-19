using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Services
{
    public interface IStatisticsService
    {
        Task<StatisticsViewModel> GetHomePageStatisticsAsync();
    }
}
