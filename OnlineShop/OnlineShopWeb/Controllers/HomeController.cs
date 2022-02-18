using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWeb.Helpers;
using OnlineShopWeb.Models;
using System.Diagnostics;

namespace OnlineShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsStorage productStorage;


        public HomeController(IProductsStorage productStorage)
        {
            this.productStorage = productStorage;
        }

        public IActionResult Index(int id)
        {
            var products = productStorage.LoadProducts();
            var productViewModels = Mapping.ToProductViewModels(products);
            return View(productViewModels);
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            var productsSearchResult = productStorage.Search(searchString);
            return View(productsSearchResult);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
