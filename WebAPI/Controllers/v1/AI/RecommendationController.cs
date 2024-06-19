using Business.Services.AI.Abstract;
using Core.Api.Abstract;
using Core.Constants;
using Core.Utils.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1.AI;

[ApiController]
public class RecommendationController : BaseController
{
    private readonly IProductRecommendationService _productRecommendationService =
        ServiceTool.GetService<IProductRecommendationService>()!;

    [HttpGet("store-products")]
    [Authorize(Roles = UserRoles.Customer)]
    public async Task<IActionResult> GetRecommendedProductsAsync()
    {
        var result = await _productRecommendationService.GetRecommendedProductsAsync();

        return result.HasFailed
            ? BadRequest(result)
            : Ok(result);
    }
}