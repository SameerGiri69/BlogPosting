using BlogApp.Backend.Data;
using BlogApp.Backend.DTOs;
using BlogApp.Backend.Interface;
using BlogApp.Backend.Mappers;
using BlogApp.Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Hosting;

namespace BlogApp.Backend.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogPostRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<bool> CreateBlog(CreateBlogDto create, AppUser currUser)
        {
            var mapped = create.ToBlogPostFromCreateBlogPostDto();
            mapped.AppUser = currUser;
            using var memoryStream = new MemoryStream();
            if (create.Image != null)
            {
                await create.Image.CopyToAsync(memoryStream);
            }
            mapped.Image = memoryStream.ToArray();
            _context.BlogPost.Add(mapped);
            return await SaveAsync();
        }

        public async Task<List<GetAllBlogDto>> GetAllBlogs()
        {
            var blogList = await _context.BlogPost.ToListAsync();
            var mapped = new List<GetAllBlogDto>();
            foreach (var blog in blogList)
            {
                mapped.Add(blog.ToGetAllBlogDtoFromBlogPost());
            }
            return mapped;
        }

        public async Task<bool> SaveAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            if (rowsAffected > 0) return true;
            return false;
        }
    }
}
