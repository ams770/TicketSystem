using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TicketSystem.API.Middleware;
using TicketSystem.Application.Auth.Commands.LoginAgent;
using TicketSystem.Application.Auth.Commands.LoginUser;
using TicketSystem.Application.Auth.Commands.RegisterAgent;
using TicketSystem.Application.Auth.Commands.RegisterUser;
using TicketSystem.Application.Categories.Queries.GetAllCategories;
using TicketSystem.Application.Comments.Commands.AddComment;
using TicketSystem.Application.Interfaces;
using TicketSystem.Application.Tickets.Commands.AssignTicket;
using TicketSystem.Application.Tickets.Commands.ChangeTicketStatus;
using TicketSystem.Application.Tickets.Commands.CreateTicket;
using TicketSystem.Application.Tickets.Queries.GetAllTickets;
using TicketSystem.Application.Tickets.Queries.GetTicketById;
using TicketSystem.Domain.Interfaces;
using TicketSystem.Infrastructure.Persistence;
using TicketSystem.Infrastructure.Persistence.Repositories;
using TicketSystem.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Database ────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ── Repositories ────────────────────────────────────────────
builder.Services.AddScoped<ITicketRepo, TicketRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IAgentRepo, AgentRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

// ── Infrastructure Services ─────────────────────────────────
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// ── Application Services — Auth ─────────────────────────────
builder.Services.AddScoped<RegisterUserService>();
builder.Services.AddScoped<RegisterAgentService>();
builder.Services.AddScoped<LoginUserService>();
builder.Services.AddScoped<LoginAgentService>();

// ── Application Services — Tickets ──────────────────────────
builder.Services.AddScoped<CreateTicketService>();
builder.Services.AddScoped<AssignTicketService>();
builder.Services.AddScoped<ChangeTicketStatusService>();
builder.Services.AddScoped<GetTicketByIdService>();
builder.Services.AddScoped<GetAllTicketsService>();
builder.Services.AddScoped<GetAllCategoriesService>();

// ── Application Services — Comments ─────────────────────────
builder.Services.AddScoped<AddCommentService>();

// ── JWT Authentication ───────────────────────────────────────
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();

// ── Swagger ──────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Allows sending JWT token from Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token here."
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// ── Middleware Pipeline ──────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication(); // ← must come before UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();