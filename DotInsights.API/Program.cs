using System.Text;
using DotInsights.API.config;
using DotInsights.API.Contracts;
using DotInsights.API.Data;
using DotInsights.API.Models;
using DotInsights.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Add DbContext to the container
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found");
builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// add IdentityCore to the container
builder.Services.AddIdentityCore<ApiUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<BlogDbContext>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials());
});

// Configure Serlilog logging
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration);
});

// Add AutoMapper to the container
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// Add Repositories to the container
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPostsRepository, PostsRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Add authentication to the container
builder.Services.AddAuthentication(options=>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


// Configure CORS
app.UseCors("AllowAll");




app.Run();
