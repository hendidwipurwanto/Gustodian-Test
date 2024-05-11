using Gusto_Test_MultiTenantApp.Models;
using Newtonsoft.Json;

namespace Gusto_Test_MultiTenantApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public List<Product> GetProducts()
        {
            var list = new List<Product>();
           // var list = new List<Order>();
            string apiUrl = _configuration.GetSection("ProductAPI_Url").Value;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {

                var result = JsonConvert.DeserializeObject<List<Product>>(response.Content.ReadAsStringAsync().Result);
                var temp = result.ToList();
                list.AddRange(temp);
            }
            return list;
        }
    }
}
