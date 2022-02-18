using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Необходимо указать название товара")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Название товара должно содержать от 2 до 50 символов")]
        public string Name { get; set; }
        
        [Required(ErrorMessage ="Необходимо указать стоимость товара")]
        [Range(0.01, Double.PositiveInfinity,ErrorMessage ="Цена товара должна быть не меньше 0,01")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Необходимо указать описание товара")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Описание товара должно содержать от 2 до 255 символов")]
        public string Descriprion { get; set; }
        public byte[] ConcurrencyToken { get; set; }
        public string ImagePath { get; set; }
    }
}
