using Microsoft.AspNetCore.Mvc;
using FutebolSimplesBetsHub.Services;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IActionResult> Index(string competition = "all", string search = "")
        {
            IEnumerable<MatchViewModel> matches;

            if (!string.IsNullOrEmpty(search))
            {
                matches = await _matchService.SearchMatchesAsync(search);
                ViewBag.SearchTerm = search;
            }
            else if (competition != "all")
            {
                matches = await _matchService.GetMatchesByCompetitionAsync(competition);
                ViewBag.SelectedCompetition = competition;
            }
            else
            {
                matches = await _matchService.GetAllMatchesAsync();
            }

            ViewBag.Matches = matches;
            ViewBag.Competitions = await _matchService.GetCompetitionsAsync();

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
    }
} 