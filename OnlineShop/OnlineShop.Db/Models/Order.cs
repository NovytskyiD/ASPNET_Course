using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public OrderStates OrderState { get; set; }

        public DateTime CreateTime { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}
