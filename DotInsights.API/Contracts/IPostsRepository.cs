using DotInsights.API.Models;

namespace DotInsights.API.Contracts;

public interface IPostsRepository : IGenericRepository<Post>
{
    // Task<ICollection<Post>> GetAllPostsAsync();
    // Task<Post> GetPostByIdAsync(int id);
    // Task<ICollection<Post>> GetPostsByUserIdAsync(int userId);
}