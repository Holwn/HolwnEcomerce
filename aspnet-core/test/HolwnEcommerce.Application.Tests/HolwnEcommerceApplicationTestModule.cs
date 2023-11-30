using Volo.Abp.Modularity;

namespace HolwnEcommerce;

[DependsOn(
    typeof(HolwnEcommerceApplicationModule),
    typeof(HolwnEcommerceDomainTestModule)
    )]
public class HolwnEcommerceApplicationTestModule : AbpModule
{

}
