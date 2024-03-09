using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Abstract;

[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
}