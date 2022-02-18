using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Db
{
    public class DbProductsStorage : IProductsStorage
    {

        private readonly DatabaseContext databaseContext;

        public DbProductsStorage(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Product> LoadProducts()
        {
            return databaseContext.Products.ToList();
        }

        public List<Product> Search(string toFind)
        {
            var searchedProducts = new List<Product>();
            if (!string.IsNullOrEmpty(toFind))
                searchedProducts = databaseContext.Products.Where(product => product.Name.ToLower().Contains(toFind.ToLower())).ToList();
            return searchedProducts;
        }

        public Product TryGetById(int id)
        {
            return databaseContext.Products.FirstOrDefault(product => product.Id == id);
        }

        public void Delete(int id)
        {
            databaseContext.Products.Remove(databaseContext.Products.FirstOrDefault(product => product.Id == id));
            databaseContext.SaveChanges();
        }

        public void Update(Product product)
        {
            var productOld = TryGetById(product.Id);
            databaseContext.Entry(productOld).Property("ConcurrencyToken").OriginalValue = product.ConcurrencyToken;
            productOld.Name = product.Name;
            productOld.ImagePath = product.ImagePath;
            productOld.Cost = product.Cost;
            productOld.Descriprion = product.Descriprion;
            databaseContext.SaveChanges();
        }

        public void Add(Product product)
        {
            product.ImagePath = "/img/default_image.jpg";
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }
    }
}
