using Blog.Service.DTOs;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(LogIn), nameof(UserController).Replace(nameof(Controller), string.Empty));
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
        public async Task<IActionResult> Registration(UserRequest request)
        {
            if (ModelState.IsValid)
            {
                UserResponse? entity = await _service.GetByEmailAsync(request.Email);

                if (entity == null)
                {
                    // registration
                    return Redirect("~/");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }

            return View(request);
        }

        [HttpPost("/User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(UserRequest request)
        {
            if (ModelState.IsValid)
            {
                UserResponse? user = await _service.GetByEmailAsync(request.Email);

                if (user != null)
                {
                    await _service.Authenticate(request);

                    return Redirect("~/");
                }

                ModelState.AddModelError("", "Lox");
            }

            return View(request);
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
