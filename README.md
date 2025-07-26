# SmartMarket Solution

SmartMarket is a modular, scalable .NET 8 solution for modern retail and analytics platforms. It provides robust APIs, business logic, data access, and domain modeling for supermarket chains, product management, customer engagement, and AI-driven recommendations.

## Solution Structure

The solution is organized into the following projects:

- **WebAPI**: ASP.NET Core Web API exposing endpoints for clients, with JWT authentication, CORS, Swagger, health checks, and request logging.
- **Business**: Contains business logic, service implementations, and AI modules (e.g., product recommendation using Accord.Math).
- **DataAccess**: Handles Entity Framework Core data access, database seeding, and migrations.
- **Domain**: Defines core domain entities, DTOs, and mapping profiles.
- **Core**: Provides shared utilities, base classes, constants, authentication helpers, dependency injection, and middleware.

## Key Features

- **Authentication & Authorization**: JWT-based authentication, role-based policies (Admin, Business, Customer), and multi-factor support.
- **Entity Framework Core**: SQL Server-backed persistence, shadow properties, soft delete, and audit trails.
- **AI Product Recommendations**: Collaborative filtering for personalized product suggestions using cosine similarity.
- **Seeding & Demo Data**: Rich initial data for customers, businesses, products, categories, and reports.
- **Modular Dependency Injection**: Extensible DI modules for each layer.
- **Logging & Monitoring**: Serilog integration, performance logging middleware, and health checks.
- **API Documentation**: Swagger UI for interactive API exploration.
- **Extensible Controllers**: Base controller for consistent API routing and structure.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (local or cloud)
- Visual Studio 2022

### Setup

1. **Clone the repository**  
   `git clone <your-repo-url>`

2. **Configure Secrets & Connection Strings**  
   - Use [User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for sensitive data.
   - Set `DefaultConnection` in `appsettings.json` or secrets.

3. **Database Migration & Seeding**  
   - On first run, the database is automatically migrated and seeded with demo data.

4. **Run the API**  
   - Set `WebAPI` as the startup project.
   - Press F5 or run `dotnet run --project WebAPI/WebAPI.csproj`.

5. **Explore the API**  
   - Visit `https://localhost:<port>/swagger` for API documentation.

## Project Highlights

### WebAPI

- **Program.cs**: Configures DI, authentication, CORS, logging, and middleware.
- **Controllers**: Inherit from `Core.Api.Abstract.BaseController` for consistent routing.

### Business

- **AI/ProductRecommendationManager**: Implements collaborative filtering for product recommendations.
- **Service Layer**: Encapsulates business rules and orchestrates domain/data access.

### DataAccess

- **EfSeeder**: Seeds the database with realistic demo data for users, products, categories, and reports.
- **EfDbContext**: Manages entity lifecycle, audit fields, and soft deletes.

### Domain

- **Entities & DTOs**: Models for products, categories, users, shopping lists, and analytics.
- **AutoMapper**: Used for mapping between entities and DTOs.

### Core

- **Utilities**: Auth helpers, DI modules, constants, error handling, and middleware.
- **Base Classes**: Abstract controller and entity base for consistency.

## Extensibility

- Add new modules by implementing `IDependencyInjectionModule`.
- Extend domain models and DTOs for new features.
- Integrate additional AI algorithms in the Business layer.

## License

This project is licensed under the MIT License.

## Contact

For questions or support, please contact [adarsonmez@outlook.com](mailto:adarsonmez@outlook.com).

---
