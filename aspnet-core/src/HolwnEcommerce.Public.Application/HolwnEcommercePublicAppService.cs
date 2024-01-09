using HolwnEcommerce.Localization;
using Volo.Abp.Application.Services;

namespace HolwnEcommerce.Public;

/* Inherit your application services from this class.
 */

public abstract class HolwnEcommercePublicAppService : ApplicationService
{
    protected HolwnEcommercePublicAppService()
    {
        LocalizationResource = typeof(HolwnEcommerceResource);
    }
}