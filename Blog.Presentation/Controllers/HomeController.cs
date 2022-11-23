using Blog.Service.DTOs;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _service;
        public IEnumerable<ArticleResponse> Articles { get; set; } = null!;

        public HomeController(ILogger<HomeController> logger, IArticleService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GetAllArticlesAsync()
        {
            IEnumerable<ArticleResponse> articles = await _service.GetAllAsync();
            Articles = articles.ToList();

            return View();
        }

        public async Task<IActionResult> CreateArticleAsync()
        {


            return View();
        }
    }
}