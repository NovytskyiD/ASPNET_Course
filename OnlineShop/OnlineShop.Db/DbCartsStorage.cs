using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbCartsStorage : ICartsStorage
    {
        private readonly DatabaseContext databaseContext;
        public DbCartsStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Cart TryGetByUserId(string userID)
        {
            return databaseContext.Carts.Include(x=>x.Items).ThenInclude(x=>x.Product).FirstOrDefault(x => x.UserId == userID);
        }

        public Cart TryGetById(Guid cartId)
        {
            return databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == cartId);
        }

        public void AddToCart(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart == null)
            {
                var newCart = new Cart()
                {
                    UserId = userId
                };

                newCart.Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Product = product,
                            Count = 1,
                            Cart=newCart
                        }
                    };
                databaseContext.Carts.Add(newCart);
            }
            else
            {
                var existingCarItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (existingCarItem != null)
                {
                    existingCarItem.Count += 1;
                }
                else
                {
                    existingCart.Items.Add(new CartItem
                    {
                        Count = 1,
                        Product = product,
                        Cart=existingCart
                    });

                }
            }
            databaseContext.SaveChanges();
        }

        public void DecreaseAmount(int productId, Guid cartID)
        {
            var existingCart = TryGetById(cartID);
            if (existingCart != null)
            {

                var existingCarItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == productId);
                if (existingCarItem != null)
                {
                    if (existingCarItem.Count > 1)
                    {
                        existingCarItem.Count -= 1;
                    }
                    else
                    {
                        existingCart.Items.Remove(existingCarItem);
                    }
                    databaseContext.SaveChanges();
                }
                
            }
        }

        public void IncreaseAmount(int productId, Guid cartID)
        {
            var existingCart = TryGetById(cartID);
            if (existingCart != null)
            {
                var existingCarItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == productId);
                if (existingCarItem != null)
                {
                    existingCarItem.Count += 1;
                    databaseContext.SaveChanges();
                }

            }
        }

        public void Clear(Guid cartID)
        {
            var existingCart = TryGetById(cartID);
            if (existingCart != null)
            {
                databaseContext.CartItems.RemoveRange(existingCart.Items);
                databaseContext.Carts.Remove(existingCart);
                databaseContext.SaveChanges();
            }
        }

        public void UpdateUserId(string oldId, string newId)
        {
            var existingCartOldUser = TryGetByUserId(oldId);
            var existingCartNewUser = TryGetByUserId(newId);
            if (existingCartOldUser != null)
            {
                if (existingCartNewUser!=null)
                {
                    foreach (var cartItem in existingCartOldUser.Items)
                    {
                        var existingCartNewUserIdItem = existingCartNewUser.Items.FirstOrDefault(x => x.Product.Id == cartItem.Product.Id);
                        if (existingCartNewUserIdItem != null)
                        {
                            existingCartNewUserIdItem.Count += cartItem.Count;
                        }
                        else
                        {
                            existingCartNewUser.Items.Add(cartItem);
                        }
                        
                    }
                    
                }
                else
                {
                    existingCartOldUser.UserId = newId;
                }
                databaseContext.SaveChanges();
                Clear(existingCartOldUser.Id);
            }
        }
    }
}
