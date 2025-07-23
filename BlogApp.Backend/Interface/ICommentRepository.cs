using BlogApp.Backend.DTOs.Comment;
using BlogApp.Backend.Models;

namespace BlogApp.Backend.Interface
{
    public interface ICommentRepository
    {
        Task<bool> CreateComment(CreateCommentDto commentDto, AppUser user);
        Task<IEnumerable<Comment>> GetAllComments(int blogId);
        //Task<bool> EditComment(EditCommentDto commentDto, int id);
        Task<bool> DeleteComment(int id);
    }
}
