using Volo.Abp.Modularity;

namespace HolwnEcommerce.Admin;

[DependsOn(
    typeof(HolwnEcommerceAdminApplicationModule),
    typeof(HolwnEcommerceDomainTestModule)
    )]
public class HolwnEcommerceAdminApplicationTestModule : AbpModule
{

}
