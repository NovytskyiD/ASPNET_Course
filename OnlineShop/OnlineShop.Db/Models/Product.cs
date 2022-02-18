using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Descriprion { get; set; }
        public string ImagePath { get; set; }
        
        [Timestamp]
        public byte[] ConcurrencyToken { get; set; }
        public List<CartItem> CartItems { get; set; }

        public List<Favorites> Favorites { get; set; }
        public List<ProductCompare> ProductCompares { get; set; }

        public Product ()
        {
            CartItems = new List<CartItem>();
            Favorites = new List<Favorites>();
            ProductCompares = new List<ProductCompare>();
        }
    }
}
