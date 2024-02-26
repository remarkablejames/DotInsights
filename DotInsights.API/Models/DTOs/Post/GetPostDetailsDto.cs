namespace DotInsights.API.Models.DTOs.Post;

public class GetPostDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public ICollection<CommentDto> Comments { get; set; } = null!;
}