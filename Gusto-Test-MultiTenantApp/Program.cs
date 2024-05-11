using Gusto_Test_MultiTenantApp.DbContexts;
using Gusto_Test_MultiTenantApp.Entities;
using Gusto_Test_MultiTenantApp.Repositories;
using Gusto_Test_MultiTenantApp.Resolvers;
using Gusto_Test_MultiTenantApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Multitenancy
builder.Services.AddMultitenancy<Tenant, TenantResolver>();

// survice and repository registration
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMonthlySalesService, MonthlySalesService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Sql Server TenantDb Connection

builder.Services.AddDbContextPool<TenantDbContext>(options => options.
        UseSqlServer(builder.Configuration.GetConnectionString("TenantConnection")));
builder.Services.AddDbContext<AppDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
// Session configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Multitenancy
app.UseMultitenancy<Tenant>();

//Use Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Signin}/{id?}");

app.Run();
