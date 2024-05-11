using Gusto_Test_MultiTenantApp.DbContexts;
using Gusto_Test_MultiTenantApp.Entities;
using Microsoft.AspNetCore.Http.Extensions;

namespace Gusto_Test_MultiTenantApp.Services
{
    public class AppUserService:IAppUserService
    {
        private readonly AppDbContext appDbContext;
        private readonly ITenantService tenantService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AppUserService(AppDbContext adbc, ITenantService tenantService, IHttpContextAccessor httpContextAccessor)
        {
            this.appDbContext = adbc;
            this.tenantService = tenantService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetTenantByUserName(string userName)
        {
            var tenant = this.tenantService.GetTenantByUserName(userName);
            if (tenant is not null)
            {
                // get current url information
                string newUrl = string.Empty;
                // get current sceme or protocol 
                string scheme = this.httpContextAccessor.HttpContext.Request.Scheme;
                newUrl += scheme + "://" + tenant.SubDomain + "." + tenant.Host + "/home/signin";
                return newUrl.ToLower();
            }
            else return null;
        }

        public string Signin(SignIn model)
        {
            var result = this.appDbContext.User.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
            if (result is not null)
            {
                // get current sceme or protocol 
                string getDisplayUrl = this.httpContextAccessor.HttpContext.Request.GetDisplayUrl();
                return $"{getDisplayUrl}home/index".ToLower();
            }
            return null;
        }
    }
}
