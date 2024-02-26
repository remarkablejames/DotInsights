namespace DotInsights.API.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}