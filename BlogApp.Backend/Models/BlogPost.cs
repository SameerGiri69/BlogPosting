using BlogApp.Backend.Models.Enums;

namespace BlogApp.Backend.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public byte[]? Image { get; set; }
        //Relationships
        public BlogCategory? Category { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<Comment>? Comments { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
