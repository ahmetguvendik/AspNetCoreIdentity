using System.ComponentModel.DataAnnotations;

namespace AG.Identity.Models
{
    
    public class CreateRoleAdminModel
    {
        [Required(ErrorMessage = "Bu Alan boş Bırakılamaz")]
        public string RoleName { get; set; }
        
    }
}
