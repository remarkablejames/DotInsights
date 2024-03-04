using AutoMapper;
using DotInsights.API.Models;
using DotInsights.API.Models.DTOs.Post;
using DotInsights.API.Models.DTOs.Users;

namespace DotInsights.API.config;

public class AutoMapperConfig: Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Post, CreatePostDto>().ReverseMap();
        CreateMap<Post, GetPostDto>().ReverseMap();
        CreateMap<Post, GetPostDetailsDto>().ReverseMap();
        CreateMap<Comment, CommentDto>().ReverseMap();

        CreateMap<ApiUser, ApiUserDto>().ReverseMap();
    }
    
}