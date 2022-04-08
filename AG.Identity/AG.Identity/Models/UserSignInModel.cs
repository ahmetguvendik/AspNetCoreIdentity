using System.ComponentModel.DataAnnotations;

namespace AG.Identity.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage ="Kullanıcı Adını Girmeniz Gerekli")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifreyi Girmeniz Gerekli")]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    
    }
}
