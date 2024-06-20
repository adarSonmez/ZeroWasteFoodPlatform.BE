using AutoMapper;
using Business.Constants.Messages.Services.Membership;
using Business.Services.Membership.Abstract;
using Core.ExceptionHandling;
using Core.Extensions;
using Core.Services.Messages;
using Core.Services.Result;
using Core.Utils.IoC;
using Core.Utils.Rules;
using DataAccess.Repositories.Abstract.Association;
using DataAccess.Repositories.Abstract.Marketing;
using DataAccess.Repositories.Abstract.Membership;
using Domain.DTOs.Marketing;
using Domain.DTOs.Membership;
using Domain.FilterModels.Membership;

namespace Business.Services.Membership.Concrete;

public class CustomerManager : ICustomerService
{
    private readonly IBusinessDal _businessDal = ServiceTool.GetService<IBusinessDal>()!;
    private readonly ICategoryDal _categoryDal = ServiceTool.GetService<ICategoryDal>()!;
    private readonly ICategoryProductDal _categoryProductDal = ServiceTool.GetService<ICategoryProductDal>()!;
    private readonly ICustomerDal _customerDal = ServiceTool.GetService<ICustomerDal>()!;

    private readonly ICustomerStoreProductDal _customerStoreProductDal =
        ServiceTool.GetService<ICustomerStoreProductDal>()!;

    private readonly IMapper _mapper = ServiceTool.GetService<IMapper>()!;
    private readonly IStoreProductDal _storeProductDal = ServiceTool.GetService<IStoreProductDal>()!;

    public async Task<ServiceObjectResult<CustomerGetDto?>> GetByIdAsync(Guid id)
    {
        var result = new ServiceObjectResult<CustomerGetDto?>();

        try
        {
            var customer = await _customerDal.GetAsync(b => id.Equals(b.Id));
            BusinessRules.Run(("CSTM-759000", BusinessRules.CheckEntityNull(customer)));

            var customerGetDto = _mapper.Map<CustomerGetDto>(customer);
            result.SetData(customerGetDto, CustomerServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CSTM-203397", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<CustomerGetDto?>> GetByUsernameAsync(string username)
    {
        var result = new ServiceObjectResult<CustomerGetDto?>();

        try
        {
            BusinessRules.Run(("CSTM-451423", BusinessRules.CheckStringNullOrEmpty(username)));

            var customer = await _customerDal.GetAsync(b => username.Equals(b.Username));
            BusinessRules.Run(("CSTM-502930", BusinessRules.CheckEntityNull(customer)));

            var customerGetDto = _mapper.Map<CustomerGetDto>(customer);
            result.SetData(customerGetDto, CustomerServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CSTM-837003", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<CustomerGetDto?>> GetByEmailAsync(string email)
    {
        var result = new ServiceObjectResult<CustomerGetDto?>();

        try
        {
            BusinessRules.Run(("CSTM-702076", BusinessRules.CheckEmail(email)));

            var customer = await _customerDal.GetAsync(b => email.Equals(b.Email));
            BusinessRules.Run(("CSTM-419335", BusinessRules.CheckEntityNull(customer)));

            var customerGetDto = _mapper.Map<CustomerGetDto>(customer);
            result.SetData(customerGetDto, CustomerServiceMessages.Retrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CSTM-230520", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<CustomerGetDto?>> UpdateAsync(CustomerUpdateDto customerUpdateDto)
    {
        var result = new ServiceObjectResult<CustomerGetDto?>();

        try
        {
            BusinessRules.Run(
                ("CSTM-377965", await CheckIfUsernameExists(customerUpdateDto.Username, true)),
                ("CSTM-842305", await CheckIfEmailExists(customerUpdateDto.Email, true))
            );

            var customerToUpdate = await _customerDal.GetAsync(b => customerUpdateDto.Id.Equals(b.Id));
            BusinessRules.Run(("CSTM-298033", BusinessRules.CheckEntityNull(customerToUpdate)));

            customerToUpdate = _mapper.Map(customerUpdateDto, customerToUpdate)!;
            await _customerDal.UpdateAsync(customerToUpdate);

            var updatedCustomer = await _customerDal.GetAsync(b => customerUpdateDto.Id.Equals(b.Id));
            var customerGetDto = _mapper.Map<CustomerGetDto>(updatedCustomer);
            result.SetData(customerGetDto, CustomerServiceMessages.Updated);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CSTM-997423", e.Message));
        }

        return result;
    }

    public async Task<ServiceObjectResult<CustomerGetDto?>> DeleteByIdAsync(Guid id)
    {
        var result = new ServiceObjectResult<CustomerGetDto?>();

        try
        {
            var customer = await _customerDal.GetAsync(b => id.Equals(b.Id));
            BusinessRules.Run(("CSTM-440740", BusinessRules.CheckEntityNull(customer)));

            await _customerDal.SoftDeleteAsync(customer!);
            result.SetData(_mapper.Map<CustomerGetDto>(customer), CustomerServiceMessages.Deleted);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CSTM-661359", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<CustomerGetDto>> GetListAsync(CustomerFilterModel? filterModel, int page,
        int pageSize)
    {
        var result = new ServiceCollectionResult<CustomerGetDto>();

        try
        {
            var filters = filterModel?.ToExpression();
            var customers = await _customerDal.GetAllAsync(filters);
            var customerGetDtos = _mapper.Map<List<CustomerGetDto>>(customers);
            result.SetData(customerGetDtos, page, pageSize, CustomerServiceMessages.ListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CSTM-304796", e.Message));
        }

        return result;
    }

    public async Task<ServiceCollectionResult<StoreProductGetDto>> GetShoppingListAsync(Guid userId)
    {
        var result = new ServiceCollectionResult<StoreProductGetDto>();

        try
        {
            var customerStoreProducts = await _customerStoreProductDal
                .GetAllAsync(b => b.CustomerId.Equals(userId));

            var storeProductsIds = customerStoreProducts.Select(b => b.ProductId).ToList();
            var products = await _storeProductDal.GetAllAsync(b => storeProductsIds.Contains(b.Id));

            foreach (var product in products)
            {
                var categoryProducts =
                    await _categoryProductDal.GetAllAsync(b => b.ProductId == product.Id);
                var categories =
                    await _categoryDal.GetAllAsync(b => categoryProducts.Select(c => c.CategoryId).Contains(b.Id));

                product.Categories = categories;

                var business = await _businessDal.GetAsync(b => b.Id.Equals(product.BusinessId));
                BusinessRules.Run(("CSTM-515321", BusinessRules.CheckEntityNull(business)));

                product.Business = business!;
            }

            var productGetDtos = _mapper.Map<List<StoreProductGetDto>>(products);
            result.SetData(productGetDtos, successMessage: CustomerServiceMessages.ShoppingListRetrieved);
        }
        catch (ValidationException e)
        {
            result.Fail(e);
        }
        catch (Exception e)
        {
            result.Fail(new ErrorMessage("CSTM-203397", e.Message));
        }

        return result;
    }

    private async Task<string?> CheckIfUsernameExists(string? username, bool isUpdate = false)
    {
        if (!isUpdate)
        {
            BusinessRules.Run(("CSTM-435045", BusinessRules.CheckStringNullOrEmpty(username)));
        }
        else
        {
            if (string.IsNullOrEmpty(username))
                return null;
        }

        var customer = await _customerDal.GetAsync(b => username!.Equals(b.Username));

        return customer is not null ? CustomerServiceMessages.UsernameAlreadyExists : null;
    }

    private async Task<string?> CheckIfEmailExists(string? email, bool isUpdate = false)
    {
        if (!isUpdate)
        {
            BusinessRules.Run(("CSTM-616020", BusinessRules.CheckEmail(email!)));
        }
        else
        {
            if (string.IsNullOrEmpty(email))
                return null;
        }

        var customer = await _customerDal.GetAsync(b => email!.Equals(b.Email));

        return customer is not null ? CustomerServiceMessages.EmailAlreadyExists : null;
    }
}