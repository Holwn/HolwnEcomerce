using HolwnEcommerce.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HolwnEcommerce.Public.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PublicController : AbpControllerBase
{
    protected PublicController()
    {
        LocalizationResource = typeof(HolwnEcommerceResource);
    }
}
