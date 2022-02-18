using System;

namespace OnlineShopWeb.Models
{
    public class OrderDetailsViewModel
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int ProductAmount { get; set; }
    }
}