using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplicationBlog.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; } 
        public DbSet<PostComment> Comments { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
                .HasMany(bp => bp.Comments) // Un BlogPost puede tener muchos PostComments
                .WithOne(pc => pc.BlogPost) // Cada PostComment pertenece a un único BlogPost
                .HasForeignKey(pc => pc.BlogPostId) 
                .OnDelete(DeleteBehavior.Cascade); 

            base.OnModelCreating(modelBuilder); 
        }
    }
}
