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

        public async Task<BlogPost> EditBlog(EditBlogPostDto updatedBlog, int blogId)
        {
            var existingBlog = await _context.BlogPost.FindAsync(blogId);
            if (existingBlog == null)
            {
                return null; // Blog not found
            }
            existingBlog.Title = updatedBlog.Title ?? existingBlog.Title;
            existingBlog.SubTitle = updatedBlog.SubTitle ?? existingBlog.SubTitle;
            existingBlog.Description = updatedBlog.Description ?? existingBlog.Description;
            existingBlog.Category = updatedBlog.Category ?? existingBlog.Category;
            if (updatedBlog.Image != null)
            {
                // Example: Assume Image is stored as a byte array or path in the database
                using (var memoryStream = new MemoryStream())
                {
                    await updatedBlog.Image.CopyToAsync(memoryStream);
                    existingBlog.Image = memoryStream.ToArray(); // If storing the image as bytes
                }
            }
            var rowsAffected = _context.SaveChanges();
            if(rowsAffected > 0)
            {
                return existingBlog;
            }
            return null;
        }

        public async Task<List<GetAllBlogDto>> GetAllBlogs()
        {
            var blogList = await _context.BlogPost.Select(blog => new GetAllBlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                SubTitle = blog.SubTitle,
                Description = blog.Description,
                Author = blog.Author,
                Image = blog.Image,
                Category = blog.Category
            }).ToListAsync();
            return blogList;
        }

        public async Task<List<GetBlogPostsByUserIdDto>> GetBlogPostsByUserId(AppUser currUser)
        {

                var userBlogs = await _context.BlogPost.Where(x => x.AppUser == currUser).Select(post => new GetBlogPostsByUserIdDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    Author = post.Author,
                }).ToListAsync();
            return userBlogs;
        }

        public async Task<bool> SaveAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            if (rowsAffected > 0) return true;
            return false;
        }
    }
}
