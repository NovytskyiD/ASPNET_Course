using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbOrdersStorage : IOrdersStorage
    {
        private readonly DatabaseContext databaseContext;
        public DbOrdersStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Order> GetAll()
        {
            return databaseContext.Orders.Include(x=>x.OrderDetails).ToList();
        }


        public void Add(string userId, Cart cart)
        {
            var order = new Order()
            {
                UserId = userId,
                OrderState = OrderStates.New,
                CreateTime = DateTime.Now,

            };

            foreach (var carItem in cart.Items)
            {
                order.OrderDetails.Add(new OrderDetails
                { ProductId = carItem.Product.Id, ProductName = carItem.Product.Name, ProductCost = carItem.Product.Cost, ProductAmount = carItem.Count, Order=order });
            }
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();
        }

        public Order TryGetById(Guid orderId)
        {
            return databaseContext.Orders.Include(x => x.OrderDetails).FirstOrDefault(order => order.Id == orderId);
        }

        public void Update(Guid orderId, OrderStates orderState)
        {
            var order = TryGetById(orderId);
            if (orderId != null)
            {
                order.OrderState = orderState;
                databaseContext.SaveChanges();
            }
        }
    }
}
