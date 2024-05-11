using Gusto_Test_MultiTenantApp.Repositories;
using Gusto_Test_MultiTenantApp.ViewModels;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Gusto_Test_MultiTenantApp.Services
{
    public class MonthlySalesService : IMonthlySalesService
    {
        private readonly IOrderRepository _orderRepository;
        public MonthlySalesService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<ViewMonthlySales> GetMonthlySalesByCountry(string country)
        {
            var list = new List<ViewMonthlySales>();
            var listWithoutTotalOrder = new List<ViewMonthlySales>();
            var listWithTotalOrder = new List<ViewMonthlySales>();
            var orderList = _orderRepository.GetOrders();
            foreach (var item in orderList)
            {
                var orderDateString = Convert.ToDateTime(item.orderDate, CultureInfo.InvariantCulture).ToString("MMM-yyyy");
                var orderDate = Convert.ToDateTime(item.orderDate, CultureInfo.InvariantCulture);
                var totalOrder = orderList.Where(w => w.orderDate.Contains(item.orderDate)).Count();
                var totRevenue = Convert.ToDecimal(item.details.FirstOrDefault().quantity * item.details.FirstOrDefault().unitPrice);
                var temp = new ViewMonthlySales() { OrderDate=orderDate, Month=orderDateString, TotalOrders=totalOrder, TotalRevenue=decimal.Round(totRevenue,2,MidpointRounding.AwayFromZero)};
               
                list.Add(temp);
            }

            var listByMonth =  list.GroupBy(g=>g.Month)
                .Select(group=> new ViewMonthlySales { Month=group.Key, TotalOrders=group.Sum(o=>o.TotalOrders), OrderDate=group.FirstOrDefault().OrderDate, TotalRevenue=group.Sum(o=>o.TotalRevenue) }).ToList();


        

            var orderedList = listByMonth.OrderBy(x => x.OrderDate.Year).ThenBy(x => x.OrderDate.Month).ToList();

        
            return orderedList;
        }
    }
}
