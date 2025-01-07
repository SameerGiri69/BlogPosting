using BlogApp.Backend.Data;
using BlogApp.Backend.DTOs;
using BlogApp.Backend.Interface;
using BlogApp.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/blog")]
    public class BlogPostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly UserManager<AppUser> _userManager;

        public BlogPostController(ApplicationDbContext context, IBlogPostRepository blogPostRepository,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _blogPostRepository = blogPostRepository;
            _userManager = userManager;
        }

        [HttpPost("index")]
        public async Task<IActionResult> PostBlog(CreateBlogDto blogDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var isSuccessful = await _blogPostRepository.CreateBlog(blogDto, await _userManager.GetUserAsync(User) );
            if (isSuccessful) return Ok("Blog created Successfully");
            return BadRequest("Something went wrong contact admin please");
        }
        [HttpGet("get-image")]
        public async Task<IActionResult> GetAllBlogs()
        {
            var posts = await _blogPostRepository.GetAllBlogs();
            var response = posts.Select(post => new
            {
                Title = post.Title,
                SubTitle = post.SubTitle,
                Description = post.Description,
                Author = post.Author,
                Category = post.Category,
                ImageBase64 = post.Image != null ? Convert.ToBase64String(post.Image) : null
            });
            return Ok(response);
        }
    }
}

