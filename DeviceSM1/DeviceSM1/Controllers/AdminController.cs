using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeviceSM1.Models;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DeviceSM1.Data;
using DeviceSM1.Models.Identity;
using System.Security.Claims;
using DeviceSM1.Models.ViewModel;

namespace DeviceSM1.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _appDbContext;

        public AdminController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("SuperAdmin"))
            {
                var adminList = await _userManager.GetUsersInRoleAsync("Admin");
                return View(adminList);
            }
            else if (User.IsInRole("Admin"))
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return RedirectToAction("Profile", "Admin", new { id = id });
            }

            return RedirectToAction("Login", "Auth");
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ComposeMail()
        {
            return View();
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Profile(int id)
        {
            var admin = await _userManager.FindByIdAsync(Convert.ToString(id));

            return View(admin);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(int id)
        {
            var admin = await _userManager.FindByIdAsync(Convert.ToString(id));
            return View(admin);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var admin = await _userManager.FindByIdAsync(Convert.ToString(id));
            var result = _userManager.DeleteAsync(admin).Result;
            return new JsonResult(new
            {
                success = result.Succeeded
            });
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, model.Password);

                if (result.Succeeded)
                {
                    var registerUser = _appDbContext.Users.Where(a => a.UserName == model.UserName).ToList()[0];
                    await _userManager.AddToRoleAsync(registerUser, "Admin");

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Create");
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _userManager.FindByNameAsync(model.UserName);

                admin.Email = model.Email;
                admin.Company = model.Company;
                admin.ContactPerson = model.ContactPerson;
                admin.Phone = model.Phone;
                admin.Mobile = model.Mobile;

                var result = await _userManager.UpdateAsync(admin);

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