using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Models;
using System.Linq;

namespace OnlineShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsStorage productStorage;

        public ProductController(IProductsStorage productsStorage)
        {
            this.productStorage = productsStorage;
        }

        public IActionResult Index()
        {
            var products = productStorage.LoadProducts();
            var productViewModels = Helpers.Mapping.ToProductViewModels(products);
            return View(productViewModels);
        }

        public IActionResult Delete(int productId)
        {
            productStorage.Delete(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int productId)
        {
            var product = productStorage.TryGetById(productId);
            var productViewModel = Helpers.Mapping.ToProductViewModel(product);
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var productToUpdate = productStorage.TryGetById(product.Id);
                if (productToUpdate==null)
                {
                    ModelState.AddModelError(string.Empty,"Невозможно сохранить изменения, товар был удален другим пользователем");
                    return View(product);
                }

                var productDb = new Product
                {
                    Id=product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Descriprion = product.Descriprion,
                    ImagePath=product.ImagePath,
                    ConcurrencyToken=product.ConcurrencyToken
                };

                try
                {
                    productStorage.Update(productDb);
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Product)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Невозможно сохранить изменения, товар был удален другим пользователем");
                    }
                    else
                    {
                        var databaseValues = (Product)databaseEntry.ToObject();
                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Текущее значение: {databaseValues.Name}");
                        }
                        if (databaseValues.Cost != clientValues.Cost)
                        {
                            ModelState.AddModelError("Cost", $"Текущее значение: {databaseValues.Cost}");
                        }
                        if (databaseValues.Descriprion != clientValues.Descriprion)
                        {
                            ModelState.AddModelError("Descriprion", $"Текущее значение: {databaseValues.Descriprion}");
                        }

                        ModelState.AddModelError(string.Empty, "Запись, которую вы хотите отредактировать была отредактирована другим пользователем. Операция редактирования была отменена. Ниже отображены изменения, которые были внесены. Если вы все равно хотите сохранить изменения - нажмите сохранить");
                        product.ConcurrencyToken = (byte[])databaseValues.ConcurrencyToken;
                        ModelState.Remove("ConcurrencyToken");
                    }
                }
            }
            return View(product);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var productDb = new Product
                {
                    Name = product.Name,
                    Cost = product.Cost,
                    Descriprion = product.Descriprion

                };
                productStorage.Add(productDb);
                return RedirectToAction("Index");
            }
            return View(product);
        }

    }
}
