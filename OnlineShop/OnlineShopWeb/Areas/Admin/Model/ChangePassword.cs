using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public class ChangePassword
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Не указан повторный пароль")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
