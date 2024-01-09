using HolwnEcommerce.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HolwnEcommerce.Public.Web.Pages;

/* Inherit your PageModel classes from this class.
 */

public abstract class HolwnEcommercePublicPageModel : AbpPageModel
{
    protected HolwnEcommercePublicPageModel()
    {
        LocalizationResourceType = typeof(HolwnEcommerceResource);
    }
}