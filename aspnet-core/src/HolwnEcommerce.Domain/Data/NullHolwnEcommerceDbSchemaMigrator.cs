using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HolwnEcommerce.Data;

/* This is used if database provider does't define
 * IHolwnEcommerceDbSchemaMigrator implementation.
 */
public class NullHolwnEcommerceDbSchemaMigrator : IHolwnEcommerceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
