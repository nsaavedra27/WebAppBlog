using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApplicationBlog.Infrastructure.Data;

public class InMemoryPostCommentRepository : IPostCommentRepository
{
    private readonly ApplicationDbContext _context;

    public InMemoryPostCommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddPostCommentAsync(PostComment postComment)
    {
        postComment.Id = Guid.NewGuid();
        await _context.Comments.AddAsync(postComment); 
        await _context.SaveChangesAsync(); 
    }

    public async Task<IEnumerable<PostComment>> GetCommentsByBlogPostIdAsync(Guid blogPostId)
    {
        return await _context.Comments
            .Where(c => c.BlogPostId == blogPostId) 
            .ToListAsync(); 
    }
}
