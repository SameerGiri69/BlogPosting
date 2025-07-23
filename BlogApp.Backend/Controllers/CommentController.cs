using BlogApp.Backend.DTOs.Comment;
using BlogApp.Backend.Interface;
using BlogApp.Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Backend.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepository, UserManager<AppUser> userManager)
        {;
            _commentRepository = commentRepository;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> PostComment(CreateCommentDto comment)
        {
            if (!ModelState.IsValid) return BadRequest("You have entered unexpected data");

            var res = await _commentRepository.CreateComment(comment, await _userManager.GetUserAsync(User));
            if (res == true) return Ok("Comment created successfully");

            return BadRequest("Internal server error");
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentByBlogId(int blogId)
        {
            if (blogId <= 0) return BadRequest("Invalid blog ID");

            var comments = await _commentRepository.GetAllComments(blogId);
            if (comments == null || !comments.Any()) return NotFound("No comments found for this blog post");

            return Ok(comments);
        }
    }
}
