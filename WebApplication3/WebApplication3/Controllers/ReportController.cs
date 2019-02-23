using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alerts()
        {
            return View();
        }

        public IActionResult Parameters()
        {
            return View();
        }
    }
}