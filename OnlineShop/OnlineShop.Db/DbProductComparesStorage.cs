using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbProductComparesStorage : IProductComparesStorage
    {
        private readonly DatabaseContext databaseContext;
        public DbProductComparesStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(string userId, Product product)
        {
            var existingProductCompare = TryGetByUserId(userId);
            if (existingProductCompare != null)
            {
                if (!existingProductCompare.Products.Contains(existingProductCompare.Products.FirstOrDefault(x => x.Id == product.Id)))
                {
                    existingProductCompare.Products.Add(product);
                }

            }
            else
            {
                var newProductCompare = new ProductCompare
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Products = new List<Product>()
                };
                newProductCompare.Products.Add(product);
                databaseContext.ProductCompares.Add(newProductCompare);

            }
            databaseContext.SaveChanges();
        }



        public void Delete(string userId, Product product)
        {
            var existingProductCompare = TryGetByUserId(userId);
            if (existingProductCompare != null)
            {
                existingProductCompare.Products.Remove(existingProductCompare.Products.FirstOrDefault(x => x.Id == product.Id));
                databaseContext.SaveChanges();
            }
        }

        public ProductCompare TryGetByUserId(string userId)
        {
            return databaseContext.ProductCompares.Include(x => x.Products).FirstOrDefault(x => x.UserId == userId);
        }

        

        public void UpdateUserId(string oldId, string newId)
        {
            var existingProductCompareOldUser = TryGetByUserId(oldId);
            if (existingProductCompareOldUser!=null)
            {
                foreach (var product in existingProductCompareOldUser.Products)
                {
                    Add(newId, product);
                }

                databaseContext.ProductCompares.Remove(existingProductCompareOldUser);
                databaseContext.SaveChanges();

            }
        }
    }
}
