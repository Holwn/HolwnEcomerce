using HolwnEcommerce.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HolwnEcommerce;

[DependsOn(
    typeof(HolwnEcommerceEntityFrameworkCoreTestModule)
    )]
public class HolwnEcommerceDomainTestModule : AbpModule
{

}
