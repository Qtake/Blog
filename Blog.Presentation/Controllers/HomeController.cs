using Blog.Service.DTOs;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IArticleService service, IUserService userService)
        {
            _logger = logger;
            _articleService = service;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<ArticleResponse> articles = await _articleService.GetAllAsync();

            return View(articles.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateArticle(ArticleRequest request)
        {
            string name = User.Identity!.Name!;
            UserResponse? user = await _userService.GetByNameAsync(name);

            if (user is null)
            {
                ModelState.AddModelError("NotAuthorized", "Trash");
                return View(request);
            }

            request.UserID = user.ID;
            await _articleService.AddAsync(request);

            return Redirect("~/");
        }

        public async Task<IActionResult> RemoveAsync(Guid id)
        {

            await _articleService.RemoveAsync(id);

            return Redirect("~/");
        }
    }
}