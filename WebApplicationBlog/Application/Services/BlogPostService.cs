using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class BlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }        
        public async Task<IEnumerable<BlogPost>> GetBlogPostsAsync(string titleFilter = null, int pageNumber = 1, int pageSize = 10)
        {
            return await _blogPostRepository.GetBlogPostsAsync(titleFilter, pageNumber, pageSize);            
        }
        public async Task<BlogPost> GetBlogPostByIdAsync(Guid id)
        {
            return await _blogPostRepository.GetBlogPostByIdAsync(id);
        }
        public async Task AddBlogPostAsync(BlogPost blogPost)
        {
            await _blogPostRepository.AddPostAsync(blogPost);
        }
    }
}
