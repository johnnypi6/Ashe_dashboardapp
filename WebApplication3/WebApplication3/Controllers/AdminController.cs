using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
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
                return RedirectToAction("Login", "Customers");
            }
        }
        public IActionResult AddAdmin()
        {
            return View();
            //if (ChkLogin() == true)
            //{
            //    string username = HttpContext.Session.GetString("username");
            //    ViewData["username"] = username;
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Customers");
            //}
        }

        public IActionResult AdminList()
        {
            if (ChkLogin() == true)
            {
                string username = HttpContext.Session.GetString("username");
                ViewData["username"] = username;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customers");
            }
        }

        public IActionResult ComposeMail()
        {
            if (ChkLogin() == true)
            {
                string username = HttpContext.Session.GetString("username");
                ViewData["username"] = username;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customers");
            }
        }

        private bool ChkLogin()
        {
            bool result = false;
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