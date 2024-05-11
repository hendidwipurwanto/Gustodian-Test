using Gusto_Test_MultiTenantApp.ViewModels;

namespace Gusto_Test_MultiTenantApp.Services
{
    public interface IProductService
    {
        List<ViewProduct> GetProductsByCountry(string country);
    }
}
