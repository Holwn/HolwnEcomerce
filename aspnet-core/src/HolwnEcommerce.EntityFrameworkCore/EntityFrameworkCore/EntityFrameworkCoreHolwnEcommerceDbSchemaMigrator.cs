using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HolwnEcommerce.Data;
using Volo.Abp.DependencyInjection;

namespace HolwnEcommerce.EntityFrameworkCore;

public class EntityFrameworkCoreHolwnEcommerceDbSchemaMigrator
    : IHolwnEcommerceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHolwnEcommerceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the HolwnEcommerceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HolwnEcommerceDbContext>()
            .Database
            .MigrateAsync();
    }
}
