using Gusto_Test_MultiTenantApp.Models;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Gusto_Test_MultiTenantApp.Repositories
{
  
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public List<Order> GetOrders()
        {
            var list = new List<Order>();
            string apiUrl = _configuration.GetSection("OrderAPI_Url").Value;
          //  string apiUrl = "https://northwind.vercel.app/api/orders";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                
                var result = JsonConvert.DeserializeObject<List<Order>>(response.Content.ReadAsStringAsync().Result);
                var temp = result.ToList();
                list.AddRange(temp);
            }
            return list;
        }
    }
}
