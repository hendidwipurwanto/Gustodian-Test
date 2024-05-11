using Gusto_Test_MultiTenantApp.Entities;
using SaasKit.Multitenancy;

namespace Gusto_Test_MultiTenantApp.Services
{
    public interface ITenantService
    {
        Tenant GetTenantBySubDomain(string subDomain);
        Tenant GetTenantByUserName(string UserName);
        Tenant GetTenantBySubDomainReturnObject(string subDomain);
    }

}
