using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWeb.Models;
using System.Linq;

namespace OnlineShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController( RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            var rolesViewModel = roles.Select(x => new RoleViewModel { Name = x.Name }).ToList();
            return View(rolesViewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RoleViewModel role)
        {
            var existingRole = roleManager.FindByNameAsync(role.Name).Result;
            if (existingRole != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует");
            }
            if (ModelState.IsValid)
            {
                roleManager.CreateAsync(new IdentityRole { Name = role.Name }).Wait();
                return RedirectToAction("Index");
            }
            return View(existingRole);
        }

        public IActionResult Delete(string roleName)
        {
            var existingRole = roleManager.FindByNameAsync(roleName).Result;
            roleManager.DeleteAsync(existingRole).Wait();
            return RedirectToAction("Index");
        }
    }
}
