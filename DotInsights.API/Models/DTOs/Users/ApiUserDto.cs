using System.ComponentModel.DataAnnotations;

namespace DotInsights.API.Models.DTOs.Users;

public class ApiUserDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [StringLength(15, ErrorMessage = "Your password is limited to {2} to {1} characters", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}