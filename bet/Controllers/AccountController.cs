using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FutebolSimplesBetsHub.Services;
using FutebolSimplesBetsHub.Models;

namespace FutebolSimplesBetsHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBetService _betService;

        public AccountController(IUserService userService, IBetService betService)
        {
            _userService = userService;
            _betService = betService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Usuário e senha são obrigatórios");
                return View();
            }

            var isValid = await _userService.ValidateUserAsync(username, password);
            if (!isValid)
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View();
            }

            var user = await _userService.GetUserByUsernameAsync(username);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "As senhas não coincidem");
                return View();
            }

            try
            {
                var user = await _userService.CreateUserAsync(name, email, username, password);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login");
            }

            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.User = user;
            ViewBag.Balance = await _betService.GetUserBalanceAsync(userId.Value);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Profile(string name, string email, string username)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login");
            }

            var user = await _userService.GetUserByIdAsync(userId.Value);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            user.Name = name;
            user.Email = email;
            user.Username = username;

            await _userService.UpdateUserAsync(user);

            TempData["Message"] = "Perfil atualizado com sucesso!";
            return RedirectToAction("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
} 