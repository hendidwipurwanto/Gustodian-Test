using Gusto_Test_MultiTenantApp.DbContexts;
using Gusto_Test_MultiTenantApp.Entities;
using SaasKit.Multitenancy;

namespace Gusto_Test_MultiTenantApp.Services
{
    public class TenantService : ITenantService
    {
        private readonly TenantDbContext tdbc;

        public TenantService(TenantDbContext tdbc)
        {
            this.tdbc = tdbc;
        }

        public Tenant GetTenantByUserName(string UserName)
        {
            var result = (from tenant in this.tdbc.Tenant
                          join user in this.tdbc.TenantUser 
                          on tenant.TenantId equals user.TenantId
                          where user.UserName == UserName
                          select new Tenant
                          {
                              TenantId = tenant.TenantId,
                              Name = tenant.Name,
                              Host = tenant.Host,
                              SubDomain = tenant.SubDomain,
                             // ConnectionString = tenant.ConnectionString
                          }).FirstOrDefault();

            return result;
        }

        public Tenant GetTenantBySubDomain(string subDomain)
        {
            var result = this.tdbc.Tenant.Where(t => t.SubDomain.ToLower() == subDomain.ToLower()).FirstOrDefault();
            return result ?? new Tenant();
        }

        public Tenant GetTenantBySubDomainReturnObject(string subDomain)
        {
            string sd = string.Format("{0}.example", subDomain);
            var result = this.tdbc.Tenant.Where(t => t.SubDomain.ToLower() == sd.ToLower()).FirstOrDefault();
            return result ?? new Tenant();
        }
    }

}