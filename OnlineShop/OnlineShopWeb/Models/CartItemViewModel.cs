using System;

namespace OnlineShopWeb.Models
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }

        public decimal Cost { get
            {
                return Product.Cost * Count;
            }
        }

    }
}
