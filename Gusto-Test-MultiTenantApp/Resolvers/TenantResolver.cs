using Gusto_Test_MultiTenantApp.Entities;
using Gusto_Test_MultiTenantApp.Services;
using SaasKit.Multitenancy;

namespace Gusto_Test_MultiTenantApp.Resolvers
{
    public class TenantResolver : ITenantResolver<Tenant>
    {
        private readonly IConfiguration configuration;
        // Gets or sets the current HttpContext. Returns null if there is no active HttpContext.
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITenantService tenantService;

        public TenantResolver(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tenantService = tenantService;
            this.configuration = configuration;
        }

        public async Task<TenantContext<Tenant>> ResolveAsync(HttpContext context)
        {   // get sub-domain form browser current url. if sub-domain is not exists then will set empty string
            string subDomainFromUrl0 = context.Request.Host.Value.ToLower() ?? string.Empty;
            string subDomainFromUrl = context.Request.Host.Value.ToLower().Split(".")[0] ?? string.Empty;
            // checking has any tenant by current sub-domain. 
            subDomainFromUrl = subDomainFromUrl0 != string.Empty ? string.Format("{0}.example", subDomainFromUrl): string.Empty;
            var result = this.tenantService.GetTenantBySubDomain(subDomainFromUrl);
            Tenant tenant = new();
            // checking has any subdomain is exists in current url
            if (!string.IsNullOrEmpty(result.SubDomain))
            {
                // checking orginal sub-domain and current url sub-domain
                if (!result.SubDomain.Equals(subDomainFromUrl)) return null; // if sub-domain is different then return null
                else
                {
                    tenant.TenantId = result.TenantId;
                    tenant.Name = result.Name;
                    tenant.Host = result.Host;
                    tenant.SubDomain = result.SubDomain;
                  //  tenant.ConnectionString = result.ConnectionString;
                    return await Task.FromResult(new TenantContext<Tenant>(tenant));
                }
            }
            else return await Task.FromResult(new TenantContext<Tenant>(tenant));

        }
    }
}
