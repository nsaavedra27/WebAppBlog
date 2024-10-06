using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPostCommentRepository
    {
        Task AddPostCommentAsync(PostComment postComment);
        Task<IEnumerable<PostComment>> GetCommentsByBlogPostIdAsync(Guid blogPostId);
    }
}