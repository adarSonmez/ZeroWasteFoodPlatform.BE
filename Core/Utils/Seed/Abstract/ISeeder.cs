using Microsoft.AspNetCore.Builder;

namespace Core.Utils.Seed.Abstract;

/// <summary>
/// Represents an interface for seeders.
/// </summary>
public interface ISeeder
{
    /// <summary>
    /// Seeds the application using the specified application builder.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    void Seed(IApplicationBuilder builder);
}