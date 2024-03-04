using DotInsights.API.Models.DTOs.Users;
using Microsoft.AspNetCore.Identity;

namespace DotInsights.API.Contracts;

public interface IAuthManager
{
    
    Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
    
}