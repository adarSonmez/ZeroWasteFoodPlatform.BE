using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Abstract;

/// <summary>
/// Base controller class for API controllers.
/// </summary>
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
}