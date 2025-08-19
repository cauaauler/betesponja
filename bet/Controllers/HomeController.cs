using Microsoft.AspNetCore.Mvc;
using FutebolSimplesBetsHub.Services;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchService _matchService;
        private readonly IStatisticsService _statisticsService;

        public HomeController(IMatchService matchService, IStatisticsService statisticsService)
        {
            _matchService = matchService;
            _statisticsService = statisticsService;
        }

        public async Task<IActionResult> Index()
        {
            var liveMatches = await _matchService.GetLiveMatchesAsync();
            var upcomingMatches = await _matchService.GetUpcomingMatchesAsync();
            var statistics = await _statisticsService.GetHomePageStatisticsAsync();

            ViewBag.LiveMatches = liveMatches;
            ViewBag.UpcomingMatches = upcomingMatches;
            ViewBag.Statistics = statistics;

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
} 