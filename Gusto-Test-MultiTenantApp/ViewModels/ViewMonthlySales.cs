namespace Gusto_Test_MultiTenantApp.ViewModels
{
    public class ViewMonthlySales
    {
        public string Month { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
