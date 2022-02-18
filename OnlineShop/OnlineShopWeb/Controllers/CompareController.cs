using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Helpers;
using System;

namespace OnlineShopWeb.Controllers
{
    public class CompareController : Controller
    {
        private readonly IProductComparesStorage productComparesStorage;
        private readonly IProductsStorage productsStorage;
        private readonly UserManager<User> userManager;

        public CompareController(IProductComparesStorage productComparesStorage, IProductsStorage productsStorage, UserManager<User> userManager)
        {
            this.productComparesStorage = productComparesStorage;
            this.productsStorage = productsStorage;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var productCompare = new ProductCompare();
            if (user == null)
            {
                var userId = Request.Cookies["userId"];
                productCompare = productComparesStorage.TryGetByUserId(userId);
            }
            else
            {
                productCompare = productComparesStorage.TryGetByUserId(user.Id);
            }
            var productCompareViewModel = Mapping.ToProductCompareViewModel(productCompare);
            return View(productCompareViewModel);
        }

        public IActionResult Add (int productId)
        {

            var productDb = productsStorage.TryGetById(productId);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                var userId = Request.Cookies["userId"];
                if (userId == null)
                {
                    userId = Guid.NewGuid().ToString();
                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Append("userId", userId, cookieOptions);

                }
                productComparesStorage.Add(userId, productDb);
            }
            else
            {
                productComparesStorage.Add(user.Id, productDb);
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult Delete(int productId)
        {
            var productDb = productsStorage.TryGetById(productId);
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user == null)
            {
                var userId = Request.Cookies["userId"];
                productComparesStorage.Delete(userId, productDb);
            }
            else
            {
                productComparesStorage.Delete(user.Id, productDb);
            }
            return RedirectToAction("Index");
        }
    }
}
