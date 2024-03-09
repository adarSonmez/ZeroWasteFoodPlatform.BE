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

        # endregion Membership
    }
}