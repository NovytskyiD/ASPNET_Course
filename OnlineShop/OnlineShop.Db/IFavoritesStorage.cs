using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public interface IFavoritesStorage
    {
        Favorites TryGetByUserId(string userID);
        void Add(string userId, Product product);
        void Delete(string userId, Product product);
    }
}
