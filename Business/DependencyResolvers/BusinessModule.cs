using Business.Services.AI.Abstract;
using Business.Services.AI.Concrete;
using Business.Services.Analytics.Abstract;
using Business.Services.Analytics.Concrete;
using Business.Services.Authentication.Abstract;
using Business.Services.Authentication.Concrete;
using Business.Services.Communication.Abstract;
using Business.Services.Communication.Concrete;
using Business.Services.Marketing.Abstract;
using Business.Services.Marketing.Concrete;
using Business.Services.Membership.Abstract;
using Business.Services.Membership.Concrete;
using Core.Utils.DI.Abstract;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers;

public sealed class BusinessModule : IDependencyInjectionModule
{
    public void Load(IServiceCollection services)
    {
        # region Membership

        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IBusinessService, BusinessManager>();
        services.AddScoped<ICustomerService, CustomerManager>();

        # endregion Membership

        # region Analytics

        services.AddScoped<IReportService, ReportManager>();

        # endregion Analytics

        # region Communication

        services.AddScoped<IMailingService, MailingManager>();

        # endregion Communication

        # region Authentication

        services.AddScoped<IAuthService, AuthManager>();

        # endregion Authentication

        # region Marketing

        services.AddScoped<IMonitoredProductService, MonitoredProductManager>();
        services.AddScoped<IStoreProductService, StoreProductManager>();
        services.AddScoped<ICategoryService, CategoryManager>();

        # endregion Marketing

        # region AI

        services.AddScoped<IProductRecommendationService, ProductRecommendationManager>();

        # endregion AI

        # region FluentValidation

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(typeof(BusinessModule).Assembly);

        # endregion FluentValidation
    }
}