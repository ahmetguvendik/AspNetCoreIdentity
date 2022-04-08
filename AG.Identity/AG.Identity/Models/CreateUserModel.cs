using System.ComponentModel.DataAnnotations;

namespace AG.Identity.Models
{
    public class CreateUserModel
    {
        [Required(ErrorMessage ="Kullanıcı Adı Girmeniz Lazım")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Şifreyi Girmeniz Lazım")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Paralo Eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage ="Lütfen Doğru Formatta Giriniz")]
        [Required(ErrorMessage ="E Mail Girmeniz Lazım")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Cinsiyet Bilgisi Girmeniz Lazım")]
        public string Gender { get; set; }
    }
}
