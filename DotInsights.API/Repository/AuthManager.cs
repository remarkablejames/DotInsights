using AutoMapper;
using DotInsights.API.Contracts;
using DotInsights.API.Models;
using DotInsights.API.Models.DTOs.Users;
using Microsoft.AspNetCore.Identity;

namespace DotInsights.API.Repository;

public class AuthManager: IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;

    public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
    {
        var user = _mapper.Map<ApiUser>(userDto);
        user.UserName = userDto.Email;
        var result =  await _userManager.CreateAsync(user, userDto.Password);
        

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }
        
        return result.Errors;
        
    }
}