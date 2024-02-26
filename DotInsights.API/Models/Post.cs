using System.ComponentModel.DataAnnotations;

namespace DotInsights.API.Models;

public class Post
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public ICollection<Comment> Comments { get; set; } = null!;
}