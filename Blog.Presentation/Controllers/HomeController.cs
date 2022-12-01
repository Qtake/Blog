using Blog.Service.DTOs;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _service;

        public HomeController(ILogger<HomeController> logger, IArticleService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<ArticleResponse> articles = await _service.GetAllAsync();

            return View(articles.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateArticle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ArticleRequest request)
        {
            string? name = User.Identity.Name;

            if (name is not null)
            {
                ArticleResponse? response = await _service.GetUserByName(name);

                if (response is not null)
                {
                    request.UserID = response.UserID;

                    await _service.AddAsync(request);
                }
            }

            return Redirect("~/");
        }

        public async Task<IActionResult> RemoveAsync(Guid id)
        {

            await _service.RemoveAsync(id);

            return Redirect("~/");
        }
    }
}