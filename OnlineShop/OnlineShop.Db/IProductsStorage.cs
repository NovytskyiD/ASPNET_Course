using OnlineShop.Db.Models;
using System.Collections.Generic;

namespace OnlineShop.Db
{
    public interface IProductsStorage
    {

        List<Product> LoadProducts(); 
        Product TryGetById(int id);

        void Delete(int id);
        void Update(Product product);
        void Add(Product product);
        List<Product> Search(string toFind);
    }
}
