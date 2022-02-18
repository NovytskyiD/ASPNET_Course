using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWeb.Helpers;

namespace OnlineShopWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsStorage productStorage;

        public ProductController(IProductsStorage productStorage)
        {
            this.productStorage = productStorage;
        }
        public IActionResult Index(int productId)
        {

            var product = productStorage.TryGetById(productId);
           
                if (product!=null)
            {
                var productViewModel = Mapping.ToProductViewModel(product);
                return View(productViewModel);
            }
                    
            return View("Error");
        }
    }
}
