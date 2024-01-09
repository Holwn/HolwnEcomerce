using Volo.Abp.Modularity;

namespace HolwnEcommerce.Public;

[DependsOn(
    typeof(HolwnEcommercePublicApplicationModule),
    typeof(HolwnEcommerceDomainTestModule)
    )]
public class HolwnEcommercePublicApplicationTestModule : AbpModule
{

}
