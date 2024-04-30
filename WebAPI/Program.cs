using System.Text;
using Business.DependencyResolvers;
using Core.Constants;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utils.DI.Abstact;
using DataAccess.DependencyResolvers;
using Domain.DependencyResolvers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;
const string corsPolicyName = "AllowOrigin";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyResolvers(
    new IDependencyInjectionModule[]
    {
        new CoreModule(),
        new DomainModule(),
        new BusinessModule(),
        new DataAccessModule()
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(AuthPolicies.AdminOnly, policy => policy.RequireRole(UserRoles.Admin))
    .AddPolicy(AuthPolicies.AdminOrBusiness, policy => policy.RequireAssertion(context =>
        context.User.IsInRole(UserRoles.Admin) || context.User.IsInRole(UserRoles.Business)))
    .AddPolicy(AuthPolicies.AdminOrCustomer, policy => policy.RequireAssertion(context =>
        context.User.IsInRole(UserRoles.Admin) || context.User.IsInRole(UserRoles.Customer)))
    .AddPolicy(AuthPolicies.AllowAll, policy => policy.RequireAssertion(context => true));

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
    .AddUserSecrets<Program>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSeeder();
}

app.UseCors(corsPolicyName);

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();