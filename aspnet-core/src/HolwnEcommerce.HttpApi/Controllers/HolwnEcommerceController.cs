using HolwnEcommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HolwnEcommerce.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HolwnEcommerceController : AbpControllerBase
{
    protected HolwnEcommerceController()
    {
        LocalizationResource = typeof(HolwnEcommerceResource);
    }
}
