using Volo.Abp.Settings;

namespace HolwnEcommerce.Settings;

public class HolwnEcommerceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HolwnEcommerceSettings.MySetting1));
    }
}
