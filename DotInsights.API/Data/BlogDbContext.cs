using DotInsights.API.Data.configurations;
using DotInsights.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace DotInsights.API.Data;

public class BlogDbContext: IdentityDbContext<ApiUser>
{
    
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
    }
    
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new RoleConfiguration());
    modelBuilder.ApplyConfiguration(new PostConfiguration());
    modelBuilder.ApplyConfiguration(new CommentConfiguration());
}
    
    
    
}