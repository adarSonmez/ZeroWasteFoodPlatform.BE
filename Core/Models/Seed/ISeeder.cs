using Microsoft.AspNetCore.Builder;

namespace Core.Models.Seed;

public interface ISeeder
{
    void Seed(IApplicationBuilder builder);
}