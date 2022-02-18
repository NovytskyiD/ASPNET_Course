using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Необходимо указать имя роли")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина названия должна быть от 2 до 50 символов")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var role = (RoleViewModel)obj;
            return Name==role.Name;
        }
    }
}
