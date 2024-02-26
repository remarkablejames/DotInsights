using DotInsights.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotInsights.API.Data;

public class BlogDbContext: DbContext
{
    
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Post> Posts { get; set; } = null!;
    
    
    
}