using Application.Interfaces;
using Domain.Entities;
using WebApplicationBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class InMemoryBlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext _context;
    public InMemoryBlogPostRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<BlogPost>> GetBlogPostsAsync()
    {
        return await _context.BlogPosts.ToListAsync();
    }   
    public async Task<IEnumerable<BlogPost>> GetBlogPostsAsync(string titleFilter, int pageNumber, int pageSize)
    {
        var query = _context.BlogPosts.Include(bp => bp.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(titleFilter))
        {
            query = query.Where(bp => bp.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase));
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<BlogPost> GetBlogPostByIdAsync(Guid id)
    {        
        return await _context.BlogPosts
            .Include(bp => bp.Comments) 
            .FirstOrDefaultAsync(bp => bp.Id == id);
    }

    public async Task AddPostAsync(BlogPost blogPost)
    {
        blogPost.Id = Guid.NewGuid(); 
        blogPost.PublishDate = DateTime.UtcNow; 
        await _context.BlogPosts.AddAsync(blogPost); 
        await _context.SaveChangesAsync(); 
    }
}
