namespace BlogApp.Backend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public BlogPost? BlogPost { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
