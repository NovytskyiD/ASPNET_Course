using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Models;
using Serilog;
using System;

namespace OnlineShopWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _sigiInManager;
        private readonly ICartsStorage cartsStorage;
        private readonly IProductComparesStorage productComparesStorage;

        public AccountController(UserManager<User> userManager, SignInManager<User> sigiInManager, ICartsStorage cartsStorage, IProductComparesStorage productComparesStorage)
        {
            _userManager = userManager;
            _sigiInManager = sigiInManager;
            this.cartsStorage = cartsStorage;
            this.productComparesStorage = productComparesStorage;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var userViewModel = Helpers.Mapping.ToUserViewModel(user);
            return View(userViewModel);
        }

        public IActionResult Login(string returnUrl )
        {
            return View(new Login { ReturnURL = returnUrl });
        }

        [HttpPost]
        public IActionResult Login(Login user)
        {
            if (ModelState.IsValid)
            {
                var result = _sigiInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false).Result;
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Такого пользователя не существует или пароль неверный");
                    return View(user);
                }
                var userId = Request.Cookies["userId"];
                var userLoggedInId = _userManager.FindByNameAsync(user.UserName).Result.Id;
                if (userId != null)
                {
                    cartsStorage.UpdateUserId(userId, userLoggedInId);
                    productComparesStorage.UpdateUserId(userId, userLoggedInId);
                }
                if (user.ReturnURL == null)
                    user.ReturnURL = "/account/index";
                return Redirect(user.ReturnURL); 
            }
            return View(user);
        }

        public IActionResult Register(string returnUrl)
        {
            if (returnUrl == null)
                returnUrl = "/account/index";
            return View(new UserProfileRegistration { ReturnURL = returnUrl });
        }

        [HttpPost]
        public IActionResult Register(UserProfileRegistration registration)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = registration.UserName, UserName = registration.UserName, LastName=registration.LastName, FirstName=registration.LastName, Phone=registration.Phone };
                var result = _userManager.CreateAsync(user, registration.Password).Result;
                if (result.Succeeded)
                {
                    _sigiInManager.SignInAsync(user, true).Wait();
                    TryAssignUserRole(user);
                    var userId = Request.Cookies["userId"];
                    if (userId != null)
                    {
                        cartsStorage.UpdateUserId(userId, user.Id);
                        productComparesStorage.UpdateUserId(userId, user.Id);
                        
                    }

                    return Redirect(registration.ReturnURL);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(registration);
        }

        private void TryAssignUserRole(User user)
        {
            try
            {
                _userManager.AddToRoleAsync(user, OnlineShop.Db.Constants.UserRoleName).Wait();
            }
            catch (Exception e)
            {

                Log.Logger.Error(e, "Ошибка присвоения роли");
            }
        }

        public IActionResult Logout()
        {
            _sigiInManager.SignOutAsync().Wait() ;
            return RedirectToAction("Index", "Home");
        }

    }
}
