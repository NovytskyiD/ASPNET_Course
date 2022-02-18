using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWeb.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public OrderStatesViewModel OrderState { get; set; }

        public DateTime CreateTime { get; set; }

        public decimal Cost {
            get
            {
                return OrderDetails.Sum(x => x.ProductCost*x.ProductAmount);
            }
        }
        
        public OrderShippingDetails OrderShippingDetails { get; set; }

        public List<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}
