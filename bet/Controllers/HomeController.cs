using Microsoft.AspNetCore.Mvc;
using FutebolSimplesBetsHub.Services;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMatchService _matchService;

        public HomeController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IActionResult> Index()
        {
            var liveMatches = await _matchService.GetLiveMatchesAsync();
            var upcomingMatches = await _matchService.GetUpcomingMatchesAsync();

            ViewBag.LiveMatches = liveMatches;
            ViewBag.UpcomingMatches = upcomingMatches;

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