using Gusto_Test_MultiTenantApp.Entities;
using Gusto_Test_MultiTenantApp.Models;
using Gusto_Test_MultiTenantApp.Repositories;
using Gusto_Test_MultiTenantApp.Services;
using Gusto_Test_MultiTenantApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gusto_Test_MultiTenantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMonthlySalesService _monthlySalesService;
        public HomeController(ILogger<HomeController> logger, IAppUserService appUserService,
            ITenantService tenantService,IOrderService orderService,IProductService productService,
            IHttpContextAccessor httpContextAccessor, IMonthlySalesService monthlySalesService,
            IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository
            )
        {
            _logger = logger;
            _appUserService = appUserService;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
            _productService = productService;
            _monthlySalesService = monthlySalesService;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            ViewBag.totalOrder = _orderRepository.GetOrders().Count;
            ViewBag.totalProduct=_productRepository.GetProducts().Count;
            ViewBag.totalCustomer=_customerRepository.GetCustomers().Count;
          
            var model = new ViewReport();
            model.OrderList = new List<ViewOrder>();
            model.ProductList = new List<ViewProduct>();
            model.MonthlySaleList = new List<ViewMonthlySales>();
            var url = _httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            var sd = url.Split(".example");
            var subDomain = sd[0].Split("http://");
            var tenant = _tenantService.GetTenantBySubDomainReturnObject(subDomain[1]);
            ViewBag.tenantName = tenant.Name;
            var products = _productService.GetProductsByCountry(subDomain[1]);
            var monthlySales = _monthlySalesService.GetMonthlySalesByCountry(subDomain[1]);
            var orders = _orderService.GetOrdersByCountry(subDomain[1]);
            model.OrderList.AddRange(orders);
            model.ProductList.AddRange(products);
            model.MonthlySaleList.AddRange(monthlySales);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult SignIn(string UserName = "")
        {
         
            ViewBag.disabled =  UserName == "" ? false : true;

            
            var none = @"style=display:none;";
            var inline = @"style=display:inline;";
            ViewBag.password = UserName == "" ? none: inline;

            ViewBag.username = UserName == "" ? inline : none;
            ViewBag.displayText = UserName == "" ? "Please Enter your username!":"Please Enter your Password to Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignIn model)
        {
      
            if (ModelState.IsValid)
            {
    
                if (model.Password is null)
                {
                   
                    var result = _appUserService.GetTenantByUserName(model.UserName);

                    if (result != null)
                    {
                        return Redirect(result + "?UserName=" + model.UserName);
                    }
                    else 
                    {
                        ViewBag.Email = string.Empty;
                        ViewBag.Error = "Provide valid User Name";
                    }
                }
                else 
                {
                    var result = _appUserService.Signin(model);
                    if (result is null)
                    {
                        ViewBag.UserName = model.UserName;
                        ViewBag.Error = "Provide valid password";
                    }
                    else
                    {
                        return Redirect(result);
                    }
                }
            }
            else ViewBag.Username = ""; 
            return View();
        }


        public IActionResult Logout()
        {


            return Redirect("http://localhost:5132");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
