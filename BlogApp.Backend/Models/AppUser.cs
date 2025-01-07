using Microsoft.AspNetCore.Identity;

namespace BlogApp.Backend.Models
{
    public class AppUser : IdentityUser
    {
        public int Id { get; set; }
        public List<BlogPost>? Posts { get; set; }
    }
}
