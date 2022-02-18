using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IOrdersStorage
    {
        List<Order> GetAll();
        void Add(string userId, Cart cart);
        Order TryGetById(Guid orderId);
        void Update(Guid orderId, OrderStates orderState);
    }
}
