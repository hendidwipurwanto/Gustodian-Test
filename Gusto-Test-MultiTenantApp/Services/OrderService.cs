using Gusto_Test_MultiTenantApp.Repositories;
using Gusto_Test_MultiTenantApp.ViewModels;
using System.Globalization;

namespace Gusto_Test_MultiTenantApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            
        }
        public List<ViewOrder> GetOrdersByCountry(string country)
        {
            var list = new List<ViewOrder>();
            var orderlist = _orderRepository.GetOrders();
            var customerList = _customerRepository.GetCustomers();
            foreach (var item in orderlist)
            {
                var cust = customerList.Where(w=>w.id==item.customerId).FirstOrDefault();
                if(cust.contactName.ToLower() == "thomas hardy")
                {
                    var abc = "123";
                }

                var orderdate= Convert.ToDateTime(item.orderDate, CultureInfo.InvariantCulture).ToString("dd MMMM yyyy");
                var total = (item.details.FirstOrDefault().unitPrice * item.details.FirstOrDefault().quantity);
                var totalString = decimal.Round((decimal)total, 2, MidpointRounding.AwayFromZero).ToString();
                var temp = new ViewOrder { OrderId = item.id.ToString(),
                    OrderDate = orderdate,
                    CustomerName = cust == null ? "" : cust.contactName,
                    Country = cust == null ? "" : cust.address.country,
                    Total = totalString};
                list.Add(temp);

            }
            return list.Where(w => w.Country.ToLower().Contains(country.ToLower())).ToList();
         //  return list;
        }
    }
}
