using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models
{
    public enum OrderStatesViewModel
    {
        [Display(Name="Новый")]
        New,
        [Display(Name = "Обработан")]
        Processed,
        [Display(Name = "В пути")]
        Shipped,
        [Display(Name = "Отменен")]
        Canceled,
        [Display(Name = "Доставлен")]
        Delivered
    }
}
