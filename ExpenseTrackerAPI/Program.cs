using Business.Abstract;
using Business.Concrete;
using DAL.Abstract;
using DAL;
using DAL.Concrete;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Context
builder.Services.AddDbContext<ExpenseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// JWT Configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenHelper, TokenHelper>();

// JWT Authentication with HttpOnly Cookie
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"🚨 JWT Doğrulama Hatası: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };




        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins("https://localhost:7203") // MVC Projesinin URL'si
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // HttpOnly Cookie desteği için gerekli!
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Use(async (context, next) =>
{
    if (context.Request.Cookies.ContainsKey("AuthToken"))
    {
        var token = context.Request.Cookies["AuthToken"];
        Console.WriteLine($"✅ Token bulundu: {token}");
    }
    else
    {
        Console.WriteLine("🚨 AuthToken Cookie API'ye ulaşmadı!");
    }

    await next();
});

app.Run();
