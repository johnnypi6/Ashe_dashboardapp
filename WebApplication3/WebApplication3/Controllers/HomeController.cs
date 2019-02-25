using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApplication3.Models;
using WebApplication3.MyClass;
using System.Data;
namespace WebApplication3.Controllers
{    
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            return View();

            if (ChkLogin() ==true)
            {
                return View();
            }
            else
            {
                //return RedirectToAction("Login", "Account", new { area = "area" });
                return RedirectToAction("Login", "Account");
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
            bool result = false;
            if(HttpContext.Session.GetString("login") != null)
            {
                if(HttpContext.Session.GetString("login") == "1")
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
