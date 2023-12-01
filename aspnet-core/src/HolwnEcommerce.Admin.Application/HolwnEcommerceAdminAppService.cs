using System;
using System.Collections.Generic;
using System.Text;
using HolwnEcommerce.Localization;
using Volo.Abp.Application.Services;

namespace HolwnEcommerce.Admin;

/* Inherit your application services from this class.
 */
public abstract class HolwnEcommerceAdminAppService : ApplicationService
{
    protected HolwnEcommerceAdminAppService()
    {
        LocalizationResource = typeof(HolwnEcommerceResource);
    }
}
