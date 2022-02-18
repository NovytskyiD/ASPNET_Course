using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DbFavoritesStorage : IFavoritesStorage
    {
        private readonly DatabaseContext databaseContext;
        public DbFavoritesStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(string userId, Product product)
        {
            var existingFavorites = TryGetByUserId(userId);
            if (existingFavorites != null)
            {
                if (!existingFavorites.Products.Contains(existingFavorites.Products.FirstOrDefault(x => x.Id == product.Id)))
                {
                    existingFavorites.Products.Add(product);
                }

            }
            else
            {
                var newFavorites = new Favorites
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Products = new List<Product>()
                };
                newFavorites.Products.Add(product);
                databaseContext.Favorites.Add(newFavorites);

            }
            databaseContext.SaveChanges();
        }

        public void Delete(string userId, Product product)
        {
            var existingFavorites = TryGetByUserId(userId);
            if (existingFavorites != null)
            {
                existingFavorites.Products.Remove(existingFavorites.Products.FirstOrDefault(x => x.Id == product.Id));
                databaseContext.SaveChanges();
            }
        }

        public Favorites TryGetByUserId(string userID)
        {
            return databaseContext.Favorites.Include(x=>x.Products).FirstOrDefault(favorite => favorite.UserId == userID);
        }
    }
}
