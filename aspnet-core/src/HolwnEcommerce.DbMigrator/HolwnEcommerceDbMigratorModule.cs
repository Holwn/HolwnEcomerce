using HolwnEcommerce.Admin;
using HolwnEcommerce.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace HolwnEcommerce.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HolwnEcommerceEntityFrameworkCoreModule),
    typeof(HolwnEcommerceAdminApplicationContractsModule)
    )]
public class HolwnEcommerceDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
