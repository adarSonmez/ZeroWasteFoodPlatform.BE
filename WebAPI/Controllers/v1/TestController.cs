using Core.Utils.IoC;
using DataAccess.Repositories.Abstract.Analytics;
using DataAccess.Repositories.Abstract.Marketing;
using DataAccess.Repositories.Abstract.Membership;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1;

// This is a test controller to make sure that the IoC container and DbContext are working properly.
// After implementing business logic, this controller will be removed.
[ApiController]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    private readonly IBusinessDal _businessDal = ServiceTool.ServiceProvider.GetService<IBusinessDal>()!;
    private readonly ICustomerDal _customerDal = ServiceTool.ServiceProvider.GetService<ICustomerDal>()!;

    private readonly IMonitoredProductDal _monitoredProductDal =
        ServiceTool.ServiceProvider.GetService<IMonitoredProductDal>()!;

    private readonly IProductDal _productDal = ServiceTool.ServiceProvider.GetService<IProductDal>()!;
    private readonly IReportDal _reportDal = ServiceTool.ServiceProvider.GetService<IReportDal>()!;
    private readonly IStoreProductDal _storeProductDal = ServiceTool.ServiceProvider.GetService<IStoreProductDal>()!;
    private readonly IUserDal _userDal = ServiceTool.ServiceProvider.GetService<IUserDal>()!;

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userDal.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("customers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerDal.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet]
    [Route("businesses")]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var businesses = await _businessDal.GetAllAsync();
        return Ok(businesses);
    }

    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productDal.GetAllAsync();
        return Ok(products);
    }

    [HttpGet]
    [Route("monitored-products")]
    public async Task<IActionResult> GetAllMonitoredProducts()
    {
        var monitoredProducts = await _monitoredProductDal.GetAllAsync();
        return Ok(monitoredProducts);
    }

    [HttpGet]
    [Route("store-products")]
    public async Task<IActionResult> GetAllStoreProducts()
    {
        var storeProducts = await _storeProductDal.GetAllAsync();
        return Ok(storeProducts);
    }

    [HttpGet]
    [Route("reports")]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _reportDal.GetAllAsync();
        return Ok(reports);
    }
}