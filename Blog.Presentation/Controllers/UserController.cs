using Blog.Service.Models;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private const string ControllerName = "/User";

        public UserController(IUserService service)
        {
            _service = service;
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
                bool isRegistered = await _service.Registration(model);

                if (isRegistered)
                {
                    return Redirect("~/");
                }

                ModelState.AddModelError("", "User with that email or Name already exist");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(AuthorizationModel model)
        {
            if (ModelState.IsValid)
            {
                bool isLogin = await _service.LogIn(model);

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
            await _service.LogOut();

            return Redirect("~/");
        }
    }
}
