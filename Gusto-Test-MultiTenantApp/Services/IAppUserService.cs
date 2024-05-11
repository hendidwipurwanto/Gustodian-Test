using Gusto_Test_MultiTenantApp.Entities;

namespace Gusto_Test_MultiTenantApp.Services
{
    public interface IAppUserService
    {
        public string GetTenantByUserName(string email);
        public string Signin(SignIn model);
    }
}
