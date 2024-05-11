using Gusto_Test_MultiTenantApp.Models;

namespace Gusto_Test_MultiTenantApp.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
    }
}
