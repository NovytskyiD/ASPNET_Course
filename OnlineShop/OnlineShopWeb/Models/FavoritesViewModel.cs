using System;
using System.Collections.Generic;

namespace OnlineShopWeb.Models
{
    public class FavoritesViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<ProductViewModel> products { get; set; }

    }
}
