using Business.Services.Analytics.Abstract;
using Business.Services.Analytics.Concrete;
using Business.Services.Authentication.Abstract;
using Business.Services.Authentication.Concrete;
using Business.Services.Communication.Abstract;
using Business.Services.Communication.Concrete;
using Business.Services.Membership.Abstract;
using Business.Services.Membership.Concrete;
using Core.Utils.DI.Abstact;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers;

public class BusinessModule : IDependencyInjectionModule

{
    public void Load(IServiceCollection services)
    {
        # region Membership

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
    }
}