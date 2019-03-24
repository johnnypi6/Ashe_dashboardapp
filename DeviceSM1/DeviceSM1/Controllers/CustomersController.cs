using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DeviceSM1.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using DeviceSM1.Data;
using DeviceSM1.Models.Identity;
using Microsoft.AspNetCore.Identity;
using DeviceSM1.Models.ViewModel;
using System.Security.Claims;

namespace DeviceSM1.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _appDbContext;

        public CustomersController(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }
        
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                var userList = await _userManager.GetUsersInRoleAsync("Customer");
                return View(userList);
            }
            else if (User.IsInRole("Customer"))
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return RedirectToAction("Profile", "Customers", new { id = id });
            }

            return RedirectToAction("Login", "Auth");
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public IActionResult Create(string success)
        {
            return View();
        }

        public async Task<IActionResult> Profile(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));

            return View(user);
        }

        public async Task<IActionResult> Modal(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));

            return new JsonResult(user);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            return View(user);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(id));
            var result = _userManager.DeleteAsync(user).Result;
            return new JsonResult(new
            {
                success = result.Succeeded
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, model.Password);

                if (result.Succeeded)
                {
                    var registerUser = _appDbContext.Users.Where(a => a.UserName == model.UserName).ToList()[0];
                    await _userManager.AddToRoleAsync(registerUser, "Customer");

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Create");
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> UpdateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                user.Email = model.Email;
                user.Company = model.Company;
                user.ContactPerson = model.ContactPerson;
                user.Phone = model.Phone;
                user.Mobile = model.Mobile;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            var id = await _userManager.GetUserIdAsync(model);

            return RedirectToAction("Update", new { id = id });
        }
    }
}