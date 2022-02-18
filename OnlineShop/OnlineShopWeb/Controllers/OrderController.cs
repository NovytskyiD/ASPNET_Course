using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;
        private readonly ICartsStorage cartsStorage;
        private readonly UserManager<User> userManager;

        public OrderController(IOrdersStorage ordersStorage, ICartsStorage cartsStorage, UserManager<User> userManager)
        {
            this.ordersStorage = ordersStorage;
            this.cartsStorage = cartsStorage;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(OrderShippingDetails orderShippingDetails)
        {
            if (ModelState.IsValid)
            {
                var cart = new Cart();
                var user = userManager.GetUserAsync(HttpContext.User).Result;
                if (user == null)
                {
                    var userId = Request.Cookies["userId"];
                    cart = cartsStorage.TryGetByUserId(userId);
                    ordersStorage.Add(userId, cart);
                }
                else
                {
                    cart = cartsStorage.TryGetByUserId(user.Id);
                    ordersStorage.Add(user.Id, cart);
                }
                cartsStorage.Clear(cart.Id);
                return RedirectToAction("Complete");
            }
            return View(orderShippingDetails);
        }
        public IActionResult Complete ()
        {
            return View();
        }
    }
}
