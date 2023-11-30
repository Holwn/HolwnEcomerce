using System;
using System.Collections.Generic;
using System.Text;
using HolwnEcommerce.Localization;
using Volo.Abp.Application.Services;

namespace HolwnEcommerce;

/* Inherit your application services from this class.
 */
public abstract class HolwnEcommerceAppService : ApplicationService
{
    protected HolwnEcommerceAppService()
    {
        LocalizationResource = typeof(HolwnEcommerceResource);
    }
}
