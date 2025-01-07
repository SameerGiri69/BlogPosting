using BlogApp.Backend.Models;
using BlogApp.Backend.Models.Enums;

namespace BlogApp.Backend.DTOs
{
    public class GetAllBlogDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public byte[]? Image { get; set; }
        //Relationships
        public BlogCategory? Category { get; set; }
    }
}
