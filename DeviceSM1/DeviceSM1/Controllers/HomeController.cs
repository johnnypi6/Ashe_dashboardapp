using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DeviceSM1.Models;

namespace DeviceSM1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            if (ChkLogin() == true)
            {
                string username = HttpContext.Session.GetString("username");
                ViewData["username"] = username;
                return View();
            }
            else
            {
                //return RedirectToAction("Login", "Customers", new { area = "area" });
                return RedirectToAction("Login", "Customers");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

        private bool ChkLogin()
        {
            //bool result = false;
            bool result = true;
            if (HttpContext.Session.GetString("login") != null)
            {
                if (HttpContext.Session.GetString("login") == "1")
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
