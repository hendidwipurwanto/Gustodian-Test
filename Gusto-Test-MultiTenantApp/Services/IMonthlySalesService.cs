using Gusto_Test_MultiTenantApp.ViewModels;

namespace Gusto_Test_MultiTenantApp.Services
{
    public interface IMonthlySalesService
    {
        List<ViewMonthlySales> GetMonthlySalesByCountry(string country);
    }
}
