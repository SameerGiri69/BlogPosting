namespace BlogApp.Backend.DTOs.Comment
{
    public class CreateCommentDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int BlogPostId { get; set; } 
        public string? UserId { get; set; } 
    }
}
