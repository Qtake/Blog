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

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, IUserService userService)
        {
            _logger = logger;
            _articleService = articleService;
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
            UserResponse user = await GetCurrentUser();
            request.UserID = user.ID;

            await _articleService.AddAsync(request);

            return Redirect("~/");
        }

        [Route("{id:Guid}/{request}")]
        public async Task<IActionResult> UpdateArticle(Guid id, ArticleRequest request)
        {
            await _articleService.UpdateAsync(id, request);

            return Redirect("~/");
        }

        [Route("{id:Guid}")]
        public async Task<IActionResult> RemoveArticle(Guid id)
        {
            await _articleService.RemoveAsync(id);

            return Redirect("~/");
        }

        private async Task<UserResponse> GetCurrentUser()
        {
            string name = User.Identity!.Name!;
            UserResponse user = (await _userService.GetByNameAsync(name))!;

            return user;
        }
    }
}