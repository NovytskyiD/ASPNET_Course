using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public class Login
    {
        [Required (ErrorMessage ="Не указан логин")]
        [EmailAddress(ErrorMessage = "Email указан неверно")]
        public string UserName {get; set;}

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string ReturnURL { get; set; }
    }
}