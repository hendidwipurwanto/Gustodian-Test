using System.ComponentModel.DataAnnotations;

namespace Gusto_Test_MultiTenantApp.Entities
{
    public class TenantUser
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string UserName { get; set; }
    }
}
