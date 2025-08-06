using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FutebolSimplesBetsHub.Services;
using FutebolSimplesBetsHub.Models.ViewModels;

namespace FutebolSimplesBetsHub.Controllers
{
    public class BetsController : Controller
    {
        private readonly IBetService _betService;
        private readonly IMatchService _matchService;

        public BetsController(IBetService betService, IMatchService matchService)
        {
            _betService = betService;
            _matchService = matchService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var bets = await _betService.GetUserBetsAsync(userId.Value);
            var balance = await _betService.GetUserBalanceAsync(userId.Value);

            ViewBag.Bets = bets;
            ViewBag.Balance = balance;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BetViewModel betViewModel)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return Json(new { success = false, message = "Usuário não autenticado" });
            }

            try
            {
                var bet = await _betService.CreateBetAsync(betViewModel, userId.Value);
                var balance = await _betService.GetUserBalanceAsync(userId.Value);

                return Json(new { 
                    success = true, 
                    message = $"Aposta de R$ {betViewModel.Amount:F2} confirmada com sucesso!",
                    balance = balance
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BetModal(int matchId, string betType, decimal odds)
        {
            var match = await _matchService.GetMatchByIdAsync(matchId);
            if (match == null)
            {
                return NotFound();
            }

            var betViewModel = new BetViewModel
            {
                MatchId = matchId,
                Participant1 = match.Participant1,
                Participant2 = match.Participant2,
                BetType = betType,
                Odds = odds
            };

            return PartialView("_BetModal", betViewModel);
        }
    }
} 