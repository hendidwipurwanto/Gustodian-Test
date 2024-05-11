using Gusto_Test_MultiTenantApp.Entities;
using SaasKit.Multitenancy;

namespace Gusto_Test_MultiTenantApp.Resolvers
{
    public interface ITenantResolver
    {
        Task<TenantContext<Tenant>> ResolveAsync(HttpContext context);
    }
}
