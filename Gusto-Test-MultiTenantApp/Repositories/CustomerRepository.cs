using Gusto_Test_MultiTenantApp.Models;
using Newtonsoft.Json;

namespace Gusto_Test_MultiTenantApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Customer> GetCustomers()
        {
            var list = new List<Customer>();
            string apiUrl = _configuration.GetSection("CustomerAPI_Url").Value; 
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {

                var result = JsonConvert.DeserializeObject<List<Customer>>(response.Content.ReadAsStringAsync().Result);
                var temp = result.ToList();
                list.AddRange(temp);
            }
            return list;
        }
    }
}
