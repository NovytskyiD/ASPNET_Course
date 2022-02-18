using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public class UserViewModel
    {
        [Required (ErrorMessage ="Не указан логин")]
        [EmailAddress(ErrorMessage = "Email указан неверно")]
        public string UserName {get; set;}

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 30 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать от 2 до 30 символов")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указан телефон")]
        [Phone(ErrorMessage = "Телефон указан неверно")]
        public string Phone { get; set; }

        public RoleViewModel Role { get; set; }             
    }
}