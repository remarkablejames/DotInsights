using DotInsights.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotInsights.API.Data.configurations;

public class PostConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Post> builder)
    {
        builder.HasData(
        
            new  { Id = 1, Title = "First Post", Content = "This is the first post", Created = DateTime.UtcNow }
        );
    }
    
}