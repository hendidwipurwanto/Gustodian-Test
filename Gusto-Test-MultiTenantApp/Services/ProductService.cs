using Gusto_Test_MultiTenantApp.Repositories;
using Gusto_Test_MultiTenantApp.ViewModels;

namespace Gusto_Test_MultiTenantApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        public ProductService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
                _productRepository = productRepository;
            _orderRepository = orderRepository;
        }
        public List<ViewProduct> GetProductsByCountry(string country)
        {
            var list = new List<ViewProduct>();
            var orderList = _orderRepository.GetOrders();
            var productList = _productRepository.GetProducts();
            foreach( var item in productList )
            {



                var order = orderList.Where(w => w.details.Any(a => a.productId == item.id)).FirstOrDefault();
                
                var ordercount = orderList.Where(w=>w.details.Any(a=>a.productId==item.id)).Count();
                var countryOfProduct = "not found";
                if(item.supplier != null)
                {
                    countryOfProduct = item.supplier.address.country;
                }
 
                var temp = new ViewProduct()
                {
                    NumberOfSold = order.details.FirstOrDefault().quantity,
                    NumberOfOrder = ordercount,
                    ProductName = item.name,
                     Country = countryOfProduct
                };

                    
                list.Add(temp);
            }

            list = list.Where(w => w.Country.ToLower().Contains(country.ToLower())).ToList();

          


            return list; 
        }
    }
}
