using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Helpers;

namespace OnlineShopWeb.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesStorage favoritesStorage;
        private readonly IProductsStorage productsStorage;
        private readonly UserManager<User> userManager;

        public FavoritesController(IFavoritesStorage favoritesStorage, IProductsStorage productsStorage, UserManager<User> userManager)
        {
            this.favoritesStorage = favoritesStorage;
            this.productsStorage = productsStorage;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var favorites = favoritesStorage.TryGetByUserId(userManager.GetUserId(HttpContext.User));
            var favoritesViewModel = Mapping.ToFavoritesViewModel(favorites);
            return View(favoritesViewModel);
        }

        public IActionResult Add(int productId)
        {
            var productDb = productsStorage.TryGetById(productId);
            favoritesStorage.Add(userManager.GetUserId(HttpContext.User), productDb);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int productId)
        {
            var productDb = productsStorage.TryGetById(productId);
            favoritesStorage.Delete(userManager.GetUserId(HttpContext.User), productDb);
            return RedirectToAction("Index");
        }
    }
}
