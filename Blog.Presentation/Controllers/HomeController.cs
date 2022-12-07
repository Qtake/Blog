using Blog.Domain.Entities;
using Blog.Service.DTOs;
using Blog.Service.Models;
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
        private readonly ITagService _tagService;

        public HomeController(
            ILogger<HomeController> logger,
            IArticleService articleService,
            IUserService userService,
            ITagService tagService)
        {
            _logger = logger;
            _articleService = articleService;
            _userService = userService;
            _tagService = tagService;
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
        public async Task<IActionResult> CreateArticle()
        {
            IEnumerable<TagResponse> tags = (await _tagService.GetAllAsync())
                .ToList();

            ViewBag.Tags = tags;

            return View();
        }

        public async Task<IActionResult> SearchByTag(string line)
        {
            IEnumerable<ArticleResponse> query = await _articleService.SearchByTag(line);

            return View("Index", query);
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
        public async Task<IActionResult> CreateArticle(ArticleCreationModel model)
        {
            var request = new ArticleRequest
            {
                Name = model.Name,
                Content = model.Content,
                Tags = new HashSet<Tag>()
            };

            if (model.TagLine is not null)
            {
                foreach (var name in model.TagLine)
                {
                    Tag? obj = await _tagService.GetByNameAsync(name);
                    request.Tags.Add(obj!);
                }
            }

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