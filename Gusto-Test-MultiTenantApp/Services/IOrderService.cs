using Gusto_Test_MultiTenantApp.ViewModels;

namespace Gusto_Test_MultiTenantApp.Services
{
    public interface IOrderService
    {
        List<ViewOrder> GetOrdersByCountry(string country);
    }
}
