using DotInsights.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DotInsights.API.Data.configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comment> builder)
    {
        builder.HasData(
            new { Id = 1, Content = "This is the first comment", Created = DateTime.UtcNow, PostId = 1 },
            new { Id = 2, Content = "This is the second comment", Created = DateTime.UtcNow, PostId = 1 }
        );
    }
}