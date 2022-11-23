using Blog.Service.DTOs;
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
            //return RedirectToAction(nameof(LogIn), nameof(UserController).Replace(nameof(Controller), string.Empty));
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
        public async Task<IActionResult> Registration(UserRequest request)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = await _service.Registration(request);

                if (isRegistered)
                {
                    return Redirect("~/");
                }
                    
                ModelState.AddModelError("", "User with that email or Name already exist");
            }

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(UserRequest request)
        {
            if (ModelState.IsValid)
            {
                bool isLogin = await _service.LogIn(request);

                if (isLogin)
                {
                    return Redirect("~/");
                }

                ModelState.AddModelError("", "Invalid Email or Password");
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
