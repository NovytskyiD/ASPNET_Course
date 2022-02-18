using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Helpers;
using OnlineShopWeb.Models;
using System;

namespace OnlineShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;

        public OrderController(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;
        }
        public IActionResult Index()
        {
            var dbOrders = ordersStorage.GetAll();
            var ordersViewModels = Mapping.ToOrderViewModels(dbOrders);
            return View(ordersViewModels);
        }

        public IActionResult Edit(Guid orderId)
        {
            var dbOrder = ordersStorage.TryGetById(orderId);
            var ordersViewModel = Mapping.ToOrderViewModel(dbOrder);
            return View(ordersViewModel);
        }

        [HttpPost]
        public IActionResult Edit(Guid orderId, OrderStatesViewModel orderState)
        {
            ordersStorage.Update(orderId, (OrderStates)orderState);
            return RedirectToAction("Index");
        }

    }
}
