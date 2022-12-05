using Blog.Service.DTOs;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            IQueryable<TagResponse> query = await _tagService.GetAllAsync();

            return View(query);
        }

        public async Task<IActionResult> Get(Guid id)
        {
            TagResponse? response = await _tagService.GetAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            return View(response);
        }

        public async Task<IActionResult> Add(TagRequest request)
        {
            await _tagService.AddAsync(request);

            return Redirect("~/");
        }

        public async Task<IActionResult> Update(Guid id, TagRequest request)
        {
            await _tagService.UpdateAsync(id, request);

            return Redirect("~/");
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            await _tagService.RemoveAsync(id);

            return Redirect("~/");
        }
    }
}
