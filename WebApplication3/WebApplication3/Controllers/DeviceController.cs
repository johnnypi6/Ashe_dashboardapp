using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddDevice()
        {
            return View();
        }
        public IActionResult ListDevice()
        {
            return View();
        }
    }
}