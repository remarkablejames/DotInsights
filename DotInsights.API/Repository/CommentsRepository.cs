using DotInsights.API.Contracts;
using DotInsights.API.Data;
using DotInsights.API.Models;

namespace DotInsights.API.Repository;

public class CommentsRepository: GenericRepository<Comment>, ICommentsRepository
{
    public CommentsRepository(BlogDbContext context) : base(context)
    {
        
    }
}