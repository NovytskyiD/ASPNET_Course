using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public class UserProfileRegistration 
    {
        [Required(ErrorMessage = "Не указан логин")]
        [StringLength(30,MinimumLength =2,ErrorMessage ="Логин должен содержать от 2 до 30 символов")]
        [EmailAddress(ErrorMessage = "Email указан неверно")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указан повторный пароль")]
        [DataType(DataType.Password)]
        [Compare ("Password", ErrorMessage ="Пароли не совпадают")]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 30 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать от 2 до 30 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        [Phone(ErrorMessage = "Телефон указан неверно")]
        public string Phone { get; set; }

        public string ReturnURL { get; set; }
    }
}
