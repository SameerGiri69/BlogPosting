using BlogApp.Backend.DTOs;
using BlogApp.Backend.Models;

namespace BlogApp.Backend.Interface
{
    public interface IBlogPostRepository
    {
        public Task<bool> CreateBlog(CreateBlogDto create, AppUser curruser);
        public Task<List<GetAllBlogDto>> GetAllBlogs();
        public Task<bool> SaveAsync();

    }
}
