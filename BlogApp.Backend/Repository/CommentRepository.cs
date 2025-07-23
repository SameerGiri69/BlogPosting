using BlogApp.Backend.Data;
using BlogApp.Backend.DTOs.Comment;
using BlogApp.Backend.Interface;
using BlogApp.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Backend.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateComment(CreateCommentDto commentDto, AppUser user)
        {
            if (commentDto == null || user == null)
            {
                return false;
            }

            // Create a new Comment object
            var comment = new Comment
            {
                Title = commentDto.Title,
                Description = commentDto.Description,
                BlogPost = await _context.BlogPost.FindAsync(commentDto.BlogPostId),
                AppUser = user
            };

            // Add the comment to the database
            await _context.Comment.AddAsync(comment);

            // Save changes to the database
            var result = await _context.SaveChangesAsync();

            return result > 0; // Return true if the comment was successfully created
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = _context.Comment.Where(x=>x.Id == id).FirstOrDefault();
            if (comment == null)
            {
                return false; // Comment not found
            }
            _context.Comment.Remove(comment);
            var res = await _context.SaveChangesAsync();
            if (res > 0) return true;
            return false;
        }

        //public Task<bool> EditComment(EditCommentDto commentDto, int id)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<IEnumerable<Comment>> GetAllComments(int blogId)
        {
            var comments = await _context.BlogPost.Where(x => x.Id == blogId).SelectMany(x => x.Comments)
                .Include(x => x.AppUser)
                .ToListAsync();
            return comments;
        }

    }
       
}
