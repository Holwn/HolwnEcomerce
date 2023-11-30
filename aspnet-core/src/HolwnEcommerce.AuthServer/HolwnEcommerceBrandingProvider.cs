using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HolwnEcommerce;

[Dependency(ReplaceServices = true)]
public class HolwnEcommerceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HolwnEcommerce";
}
