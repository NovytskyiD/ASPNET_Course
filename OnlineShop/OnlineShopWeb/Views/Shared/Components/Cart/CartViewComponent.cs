using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Helpers;


namespace OnlineShopWeb.Views.Shared.Components.CartShared
{
    public class CartViewComponent: ViewComponent
    {
        private readonly ICartsStorage cartStorage;
        private readonly UserManager<User> userManager;

        public CartViewComponent(ICartsStorage cartStorage, UserManager<User> userManager)
        {
            this.cartStorage = cartStorage;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var cart = new Cart();
            if (user == null)
            {
                var userId = Request.Cookies["userId"];
                cart = cartStorage.TryGetByUserId(userId);
            }
            else
            {
                cart = cartStorage.TryGetByUserId(user.Id);
            }
            var cartViewModel = Mapping.ToCartViewModel(cart);
            var productCount = cartViewModel?.Items.Count ?? 0;
            return View("Cart",productCount);
        }
    }
}
