using Blog.Service.DTOs;
using Blog.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]/{id}", Name = nameof(EditComment))]
        public async Task<IActionResult> EditComment(Guid id)
        {
            CommentResponse? response = await _commentService.GetAsync(id);

            if (response is null)
            {
                return NotFound();
            }

            ViewBag.Comment = response;

            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("[action]/{articleId}", Name = nameof(CreateComment))]
        public async Task<IActionResult> CreateComment(CommentRequest request, Guid articleId)
        {
            UserResponse user = await GetCurrentUser();
            request.UserID = user.ID;
            request.ArticleId = articleId;

            await _commentService.AddAsync(request);

            return Redirect("~/");
        }

        [Route("[action]/{commentId}/{UserId}/{articleId}", Name = nameof(UpdateComment))]
        public async Task<IActionResult> UpdateComment(Guid commentId, Guid userId, Guid articleId, CommentRequest request)
        {
            request.UserID = userId;
            request.ArticleId = articleId;
            await _commentService.UpdateAsync(commentId, request);

            return Redirect("~/");
        }

        [Route("[action]/{id}", Name = nameof(RemoveComment))]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveAsync(id);

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
