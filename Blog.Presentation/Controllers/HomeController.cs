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

        public HomeController(
            ILogger<HomeController> logger,
            IArticleService articleService,
            IUserService userService)
        {
            _logger = logger;
            _articleService = articleService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ArticleResponse> query = (await _articleService.GetAllAsync())
                .ToList();

            return View(query);
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

        [HttpGet]
        [Route("[action]/{id}", Name = nameof(ArticleDetails))]
        public async Task<IActionResult> ArticleDetails(Guid id)
        {
            ArticleResponse? response = await _articleService.IncludeAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            ViewBag.Article = response;

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

        [Route("[action]/{id}", Name = nameof(EditArticle))]
        public async Task<IActionResult> EditArticle(Guid id)
        {
            ArticleResponse? response = await _articleService.GetAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            ViewBag.Article = response;

            return View();
        }
        
        [Route("[action]/{articleId}/{userId}", Name = nameof(UpdateArticle))]
        public async Task<IActionResult> UpdateArticle(Guid articleId, Guid userId, ArticleRequest request)
        {
            request.UserID = userId;
            await _articleService.UpdateAsync(articleId, request);

            return Redirect("~/");
        }

        [Route("[action]/{id}", Name = nameof(RemoveArticle))]
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