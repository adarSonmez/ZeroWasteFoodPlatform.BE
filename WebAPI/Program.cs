using Core.DependencyResolvers;
using Core.Extensions;
using Core.Models.DI;
using DataAccess.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
const string corsPolicyName = "AllowOrigin";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyResolvers(new IDependencyInjectionModule[]
{
    new CoreModule(),
    new DataAccessModule()
});

builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
    .AddUserSecrets<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSeeder();
}

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();