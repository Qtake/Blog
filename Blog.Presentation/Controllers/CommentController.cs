using Blog.Service.DTOs;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            IQueryable query = await _commentService.GetAllAsync();

            return View(query);
        }

        public async Task<IActionResult> Get(Guid id)
        {
            CommentResponse? response = await _commentService.GetAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            return View(response);
        }

        public async Task<IActionResult> Add(CommentRequest request)
        {
            await _commentService.AddAsync(request);

            return Redirect("~/");
        }

        public async Task<IActionResult> Update(Guid id, CommentRequest request)
        {
            await _commentService.UpdateAsync(id, request);

            return Redirect("~/");
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            await _commentService.RemoveAsync(id);

            return Redirect("~/");
        }
    }
}
