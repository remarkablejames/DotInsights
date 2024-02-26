namespace DotInsights.API.Models.DTOs.Post;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public int PostId { get; set; }
}