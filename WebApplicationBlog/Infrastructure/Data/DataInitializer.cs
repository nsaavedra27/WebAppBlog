using Domain.Entities;

namespace WebApplicationBlog.Infrastructure.Data
{
    public static class DataInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {            
            if (!context.BlogPosts.Any())
            {
                var blogPost1Id = Guid.NewGuid();
                var blogPost2Id = Guid.NewGuid();

                var blogPost1 = new BlogPost
                {
                    Id = blogPost1Id, 
                    Title = "Introducción a la Arquitectura Limpia",
                    PublishDate = DateTime.UtcNow.AddDays(-10),
                    Comments = new List<PostComment>
                    {
                        new PostComment
                        {
                            Id = Guid.NewGuid(),
                            BlogPostId = blogPost1Id, 
                            UserFullName = "Juan Pérez",
                            Comment = "¡Excelente artículo!"
                        }
                    }
                };

                var blogPost2 = new BlogPost
                {
                    Id = blogPost2Id, 
                    Title = "Técnicas Avanzadas en .NET",
                    PublishDate = DateTime.UtcNow.AddDays(-5),
                    Comments = new List<PostComment>
                    {
                        new PostComment
                        {
                            Id = Guid.NewGuid(),
                            BlogPostId = blogPost2Id, 
                            UserFullName = "Natalia S",
                            Comment = "Gracias por la información, muy completo el artículo!"
                        }
                    }
                };

                await context.BlogPosts.AddRangeAsync(blogPost1, blogPost2);
                await context.SaveChangesAsync(); 
            }
        }
    }
}
