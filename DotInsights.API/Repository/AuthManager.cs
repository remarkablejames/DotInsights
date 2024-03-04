using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DotInsights.API.Contracts;
using DotInsights.API.Models;
using DotInsights.API.Models.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DotInsights.API.Repository;

public class AuthManager: IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
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

    public async Task<AuthResponseDto> Login(LoginUserDto userDto)
    {
        var user = await _userManager.FindByEmailAsync(userDto.Email);
        bool isValidUser = user != null && await _userManager.CheckPasswordAsync(user, userDto.Password);
        
        
        if(!isValidUser)
        {
            return null;
        }
        
        var token = await GenerateToken(user);
        return new AuthResponseDto
        {
            Token = token,
            UserId = user.Id
        };
        
    }

    private async Task<string> GenerateToken(ApiUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:key"]!));
        var credentials =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x=> new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("userId", user.Id)
            
        }.Union(userClaims).Union(roleClaims);
        
        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            null,
            expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtSettings:DurationsInMinutes"])),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}