using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace HolwnEcommerce.Emailing
{
    public class EmailSettingProvider : SettingDefinitionProvider
    {
        private readonly ISettingEncryptionService encryptionService;

        public EmailSettingProvider(ISettingEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
        }

        public override void Define(ISettingDefinitionContext context)
        {
            var passSetting = context.GetOrNull("Abp.Mailing.Smtp.Password");
            if (passSetting != null)
            {
                string debug = encryptionService.Encrypt(passSetting, "78bd09a6bd4a86468cd98e1e658185d9-4c955d28-32099d6b");
            }

        }
    }
}
