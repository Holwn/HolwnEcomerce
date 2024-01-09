using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HolwnEcommerce.Public.Web;

[Dependency(ReplaceServices = true)]
public class HolwnEcommercePublicBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Public";
}
