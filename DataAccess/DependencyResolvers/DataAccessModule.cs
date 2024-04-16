using Core.Utils.DI.Abstact;
using Core.Utils.Seed.Abstract;
using DataAccess.Context.EntityFramework;
using DataAccess.Repositories.Abstract.Analytics;
using DataAccess.Repositories.Abstract.Association;
using DataAccess.Repositories.Abstract.Marketing;
using DataAccess.Repositories.Abstract.Membership;
using DataAccess.Repositories.Concrete.EntityFramework.Analytics;
using DataAccess.Repositories.Concrete.EntityFramework.Association;
using DataAccess.Repositories.Concrete.EntityFramework.Marketing;
using DataAccess.Repositories.Concrete.EntityFramework.Membership;
using DataAccess.Utils.Seed.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyResolvers;

public class DataAccessModule : IDependencyInjectionModule
{
    public void Load(IServiceCollection services)
    {
        #region DbContext

        services.AddDbContext<EfDbContext>();

        #endregion DbContext

        #region Repositories

        services.AddScoped(typeof(IReportDal), typeof(EfReportDal));
        services.AddScoped(typeof(ICategoryProductDal), typeof(EfCategoryProductDal));
        services.AddScoped(typeof(ICategoryDal), typeof(EfCategoryDal));
        services.AddScoped(typeof(IMonitoredProductDal), typeof(EfMonitoredProductDal));
        services.AddScoped(typeof(IStoreProductDal), typeof(EfStoreProductDal));
        services.AddScoped(typeof(IProductDal), typeof(EfProductDal));
        services.AddScoped(typeof(IUserDal), typeof(EfUserDal));
        services.AddScoped(typeof(IBusinessDal), typeof(EfBusinessDal));
        services.AddScoped(typeof(ICustomerDal), typeof(EfCustomerDal));

        #endregion Repositories

        #region Utils

        services.AddScoped(typeof(ISeeder), typeof(EfSeeder));

        #endregion Utils
    }
}