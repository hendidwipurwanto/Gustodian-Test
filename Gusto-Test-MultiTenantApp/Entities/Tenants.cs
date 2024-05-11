using System.ComponentModel.DataAnnotations;

namespace Gusto_Test_MultiTenantApp.Entities
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string SubDomain { get; set; }

       // public string ConnectionString { get; set; }
    }
}
