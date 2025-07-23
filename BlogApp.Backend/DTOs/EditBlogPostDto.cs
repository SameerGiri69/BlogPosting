using BlogApp.Backend.Models.Enums;

namespace BlogApp.Backend.DTOs
{
    public class EditBlogPostDto
    {
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public BlogCategory? Category { get; set; }

    }
}
