using HolwnEcommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HolwnEcommerce.Admin.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HolwnEcommerceAdminController : AbpControllerBase
{
    protected HolwnEcommerceAdminController()
    {
        LocalizationResource = typeof(HolwnEcommerceResource);
    }
}
