using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddAdmin()
        {
            return View();
        }

        public IActionResult AdminList()
        {
            return View();
        }

        public IActionResult ComposeMail()
        {
            return View();
        }
    }
}