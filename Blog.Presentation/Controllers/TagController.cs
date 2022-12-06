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

        public async Task<IActionResult> Index()
        {
            IEnumerable<TagResponse> query = (await _tagService.GetAllAsync())
                .ToList();

            ViewBag.Tags = query;

            return View();
        }

        [Route("[action]/{id}", Name = nameof(TagDetails))]
        public async Task<IActionResult> TagDetails(Guid id)
        {
            TagResponse? response = await _tagService.GetAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            ViewBag.Tag = response;

            return View();
        }

        [Route("[action]/{id}", Name = nameof(EditTag))]
        public async Task<IActionResult> EditTag(Guid id)
        {
            TagResponse? response = await _tagService.GetAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            ViewBag.Tag = response;

            return View();
        }

        public async Task<IActionResult> CreateTag(TagRequest request)
        {
            await _tagService.AddAsync(request);

            return Redirect("Index");
        }

        [Route("[action]/{id}", Name = nameof(UpdateTag))]
        public async Task<IActionResult> UpdateTag(Guid id, TagRequest request)
        {
            await _tagService.UpdateAsync(id, request);

            return Redirect("~/");
        }

        [Route("[action]/{id}", Name = nameof(RemoveTag))]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            await _tagService.RemoveAsync(id);

            return Redirect("~/");
        }
    }
}
