using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ZimojiAssessmentProject.DataAccessLayer;
using ZimojiAssessmentProject.InterfaceImplementations;
using ZimojiAssessmentProject.Interfaces;
using ZimojiAssessmentProject.Controllers;
using ZimojiAssessmentProject.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZimojiAssessmentProject.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IUserTaskActions, UserTaskImplementations>();
builder .Services.AddScoped<IAuthenticationService, AuthenticationService>();


builder.Services.AddDbContext<UserTaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserTaskConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HeyCoach API",
        Version = "v1",
        Description = "API documentation for HeyCoach Assignment with JWT Authentication"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token.\nExample: Bearer abc123"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "HeyCoach API v1");
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.MapUserTasksEndpoints();
app.Run();
