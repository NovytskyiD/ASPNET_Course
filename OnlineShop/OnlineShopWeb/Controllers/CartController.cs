using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Helpers;
using System;

namespace OnlineShopWeb.Controllers
{

    public class CartController : Controller
    {
        private readonly IProductsStorage productStorage;
        private readonly ICartsStorage cartsStorage;
        private readonly UserManager<User> userManager;

        public CartController(IProductsStorage productStorage, ICartsStorage cartStorage, UserManager<User> userManager)
        {
            this.productStorage = productStorage;
            this.cartsStorage = cartStorage;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var cart = new Cart();
            if (user==null)
            {
                var userId = Request.Cookies["userId"];
                cart = cartsStorage.TryGetByUserId(userId);
            }
            else
            {
                cart = cartsStorage.TryGetByUserId(user.Id);
            }
            var cartViewModel = Mapping.ToCartViewModel(cart);
            return View(cartViewModel);
        }

        public IActionResult AddToCart(int productid)
        {
            var productDb= productStorage.TryGetById(productid);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
               var  userId = Request.Cookies["userId"];
                if (userId==null)
                {
                    userId = Guid.NewGuid().ToString();
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Append("userId", userId, cookieOptions);
                    
                }
                cartsStorage.AddToCart(productDb, userId);
            }
            else
            {
                cartsStorage.AddToCart(productDb, user.Id);
            }
            

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseAmount(int productId, Guid cartId)
        {
            cartsStorage.IncreaseAmount(productId, cartId);

            return RedirectToAction("Index");
        }

        public IActionResult DecreaseAmount(int productId, Guid cartId)
        {
            cartsStorage.DecreaseAmount(productId, cartId);

            return RedirectToAction("Index");
        }

        public IActionResult Clear(Guid cartId)
        {
            cartsStorage.Clear(cartId);
            return RedirectToAction("Index");
        }
    }
}
