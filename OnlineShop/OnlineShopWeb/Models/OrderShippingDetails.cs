using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public class OrderShippingDetails
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 30 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать от 2 до 30 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указан адрес")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Адрес должен содержать от 10 до 255 символов")]
        public string Adress { get; set; }

        [Required(ErrorMessage ="Не указан email")]
        [EmailAddress(ErrorMessage ="Email указан неверно")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Не указан телефон")]
        [Phone(ErrorMessage ="Телефон указан неверно")]
        public string Phone { get; set; }
    }
}
