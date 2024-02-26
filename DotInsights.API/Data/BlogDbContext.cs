using DotInsights.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotInsights.API.Data;

public class BlogDbContext: DbContext
{
    
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Post>().HasData(
        
        new  { Id = 1, Title = "First Post", Content = "This is the first post", Created = DateTime.UtcNow }
    );
    
    modelBuilder.Entity<Comment>().HasData(
        new { Id = 1, Content = "This is the first comment", Created = DateTime.UtcNow, PostId = 1 },
        new { Id = 2, Content = "This is the second comment", Created = DateTime.UtcNow, PostId = 1 }
    );
}
    
    
    
}