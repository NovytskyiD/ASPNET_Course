using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.Users.ToList();
            var userViewModel = user.Select(x=>Helpers.Mapping.ToUserViewModel(x)).ToList();
            return View(userViewModel);
        }
        public IActionResult Details(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var userViewModel = Helpers.Mapping.ToUserViewModel(user);
            return View(userViewModel);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserProfileRegistration registration)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userManager.FindByNameAsync(registration.UserName).Result;
                if (existingUser!=null)
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже существует");
                    return View(registration);
                }
                var user = new User() { UserName = registration.UserName,  FirstName = registration.FirstName, LastName = registration.LastName, Phone = registration.Phone };
                _userManager.CreateAsync(user, registration.Password).Wait();
                return RedirectToAction("Index");
            }
            return View(registration);
        }

        public IActionResult ChangePassword(string userName)
        {
            var changePassword = new ChangePassword() { UserName = userName };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)

        {

            if (ModelState.IsValid)
            {
                var existingUser = _userManager.FindByNameAsync(changePassword.UserName).Result;
                if (existingUser != null)
                {
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(existingUser,changePassword.NewPassword);
                    existingUser.PasswordHash = newPasswordHash;
                    _userManager.UpdateAsync(existingUser).Wait();
                    return RedirectToAction("Index");
                }
            }

            return View(changePassword);
        }

        public IActionResult Edit(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var editUser = Helpers.Mapping.ToEditUser(user);
            return View(editUser);
        }

        [HttpPost]
        public IActionResult Edit(string oldUserName, EditUser userProfile)

        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(oldUserName).Result;
                if (oldUserName != userProfile.UserName)
                {
                    var existingUser = _userManager.FindByNameAsync(userProfile.UserName).Result;
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("", "Пользователь с таким именем уже существует");
                        return View(userProfile);
                    }
                    user.UserName = userProfile.UserName;
                    user.Email = userProfile.UserName;

                }

                user.FirstName = userProfile.FirstName;
                user.LastName = userProfile.LastName;
                user.Phone = userProfile.Phone;
                _userManager.UpdateAsync(user).Wait();
                return RedirectToAction("Index");
            }

            return View(userProfile);
        }
        public IActionResult Delete(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            _userManager.DeleteAsync(user).Wait();
            return RedirectToAction("Index");
        }

        public IActionResult SetUserRoles(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var allRoles = roleManager.Roles.ToList();
            var SetUserRolesViewModel = new SetUserRolesViewModel
            {
                UserName = userName,
                UserRoles = userRoles.Select(x => new RoleViewModel { Name = x }).ToList(),
                AllRoles = allRoles.Select(x => new RoleViewModel { Name = x.Name }).ToList()
            };
            return View(SetUserRolesViewModel);
        }
        [HttpPost]
        public IActionResult SetUserRoles(string userName, Dictionary<string,bool> userRolesViewModel)
        {
            var UserSelectedRoles = userRolesViewModel.Select(x => x.Key);
            var user = _userManager.FindByNameAsync(userName).Result;
            var UserRoles = _userManager.GetRolesAsync(user).Result;

            _userManager.RemoveFromRolesAsync(user,UserRoles).Wait();
            _userManager.AddToRolesAsync(user, UserSelectedRoles).Wait();
            return RedirectToAction("Details",new { userName = userName });
        }
    }
}
