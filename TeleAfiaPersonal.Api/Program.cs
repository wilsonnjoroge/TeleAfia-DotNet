
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;
using TeleAfiaPersonal.Application.Authentication.Command.Register;
using TeleAfiaPersonal.Application.Common.interfaces.Authentication;
using TeleAfiaPersonal.Infrastructure.Authentication;
using TeleAfiaPersonal.Infrastructure.Repositories;
using MediatR;
using AutoMapper;
using TeleAfiaPersonal.Infrastructure;
using TeleAfiaPersonal.Contracts.AuthenticationDTOs;
using TeleAfiaPersonal.Application.Common.Interfaces; // Import AutoMapper namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext and configure SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); // Register MediatR for services from the executing assembly

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(RegisterCommandHandler)); // Register AutoMapper for services from the executing assembly

// Register RegisterCommandHandler
builder.Services.AddTransient<IRequestHandler<RegisterCommand, AuthenticationResponse>, RegisterCommandHandler>();
// Register IUserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register JwtTokenGenerator
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = jwtSettings.Issuer, // Set valid issuer
            ValidateAudience = false,
            ValidAudience = jwtSettings.Audience, // Set valid audience
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero // Adjust if necessary
        };
    });

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
