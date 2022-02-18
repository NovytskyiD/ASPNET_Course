using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
    public interface ICartsStorage
    {
        Cart TryGetByUserId(string userID);
        public Cart TryGetById(Guid cartId);
        void AddToCart(Product product, string userId);
        void DecreaseAmount(int productId, Guid cartId);
        void IncreaseAmount(int productId, Guid cartId);
        void Clear(Guid cartId);
        void UpdateUserId(string oldId, string newId);
    }
}
