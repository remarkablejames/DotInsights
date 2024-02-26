using System.ComponentModel.DataAnnotations;

namespace DotInsights.API.Models.DTOs.Post;

public class CreatePostDto
{
    [MinLength(8)]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    
}