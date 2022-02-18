using System;

namespace OnlineShop.Db.Models
{
    public class OrderDetails
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int ProductAmount { get; set; }
        public Order Order { get; set; }
    }
}