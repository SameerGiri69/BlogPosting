using BlogApp.Backend.DTOs;
using BlogApp.Backend.Models;

namespace BlogApp.Backend.Mappers
{
    public static class BlogMapper
    {
        public static BlogPost ToBlogPostFromCreateBlogPostDto(this CreateBlogDto dto)
        {
            return new BlogPost()
            {
                Title = dto.Title,
                SubTitle = dto.SubTitle,
                Description = dto.Description,
                Author = dto.Author,
            };
        }
        public static GetAllBlogDto ToGetAllBlogDtoFromBlogPost(this BlogPost posts)
        {
            return new GetAllBlogDto()
            {
                Id = posts.Id,
                Title = posts.Title,
                SubTitle = posts.SubTitle,
                Description = posts.Description,
                Author = posts.Author,
                Category = posts.Category,
                Image = posts.Image,
            };
        }
        public static GetBlogPostsByUserIdDto ToGetBlogPostsByUserIdDtoFromBlogPosts(this BlogPost posts)
        {
            return new GetBlogPostsByUserIdDto()
            {
                Id = posts.Id,
                Title = posts.Title,
                SubTitle = posts.SubTitle,
                Description = posts.Description,
                Author = posts.Author,
                Category = posts.Category,
                Image = posts.Image,
            };
        }
    }
}
