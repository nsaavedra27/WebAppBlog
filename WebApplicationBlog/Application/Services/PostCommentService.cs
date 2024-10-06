using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class PostCommentService
    {
        private readonly IPostCommentRepository _postCommentRepository;

        public PostCommentService(IPostCommentRepository postCommentRepository)
        {
            _postCommentRepository = postCommentRepository;
        }

        public async Task AddPostCommentAsync(PostComment comment)
        {
            await _postCommentRepository.AddPostCommentAsync(comment);
        }

        public async Task<IEnumerable<PostComment>> GetCommentsByBlogPostIdAsync(Guid blogPostId)
        {
            return await _postCommentRepository.GetCommentsByBlogPostIdAsync(blogPostId);
        }
    }
}
