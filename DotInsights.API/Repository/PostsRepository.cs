using DotInsights.API.Contracts;
using DotInsights.API.Data;
using DotInsights.API.Models;

namespace DotInsights.API.Repository;

public class PostsRepository: GenericRepository<Post> , IPostsRepository
{
    public PostsRepository(BlogDbContext context) : base(context)
    {
    }
    
}