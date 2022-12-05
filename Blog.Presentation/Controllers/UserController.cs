using Blog.Service.Models;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private const string ControllerName = "/User";

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(LogIn), ControllerName);
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = await _userService.Registration(model);

                if (isRegistered)
                {
                    return Redirect("~/");
                }

                ModelState.AddModelError("", "User with that Name or Email already exist");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(AuthorizationModel model)
        {
            if (ModelState.IsValid)
            {
                bool isLogin = await _userService.LogIn(model);

                if (isLogin)
                {
                    return Redirect("~/");
                }

                ModelState.AddModelError("", "Invalid Name or Password");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOut();

            return Redirect("~/");
        }
    }
}
