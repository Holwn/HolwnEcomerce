using System.Threading.Tasks;

namespace HolwnEcommerce.Data;

public interface IHolwnEcommerceDbSchemaMigrator
{
    Task MigrateAsync();
}
