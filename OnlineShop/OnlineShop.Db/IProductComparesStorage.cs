using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IProductComparesStorage
    {
        ProductCompare TryGetByUserId(string userId);
        void Add(string userId, Product product);
        void Delete(string userId, Product product);
        void UpdateUserId(string oldId, string newId);
    }

}
