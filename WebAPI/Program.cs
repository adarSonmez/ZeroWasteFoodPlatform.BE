using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utils.IoC;
using DataAccess.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

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
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSeeder();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();