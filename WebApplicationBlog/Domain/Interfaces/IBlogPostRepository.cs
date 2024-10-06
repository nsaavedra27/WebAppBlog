using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetBlogPostsAsync(string titleFilter, int pageNumber, int pageSize);
        Task<BlogPost> GetBlogPostByIdAsync(Guid id);
        Task AddPostAsync(BlogPost blogPost);
    }
}


