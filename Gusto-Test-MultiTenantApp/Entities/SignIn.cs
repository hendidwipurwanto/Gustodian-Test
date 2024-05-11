using System.ComponentModel.DataAnnotations;

namespace Gusto_Test_MultiTenantApp.Entities
{
    public class SignIn
    {
        [Required(ErrorMessage = "email address is required")]
      //  [EmailAddress]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
