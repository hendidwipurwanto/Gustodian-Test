using System.ComponentModel.DataAnnotations;

namespace Gusto_Test_MultiTenantApp.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
