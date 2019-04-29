using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DeviceSM1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DeviceSM1.Data;
using DeviceSM1.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeviceSM1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _appDbContext;

        public HomeController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alert(int? id)
        {
            if (id == null)
            {
                var alerts = _appDbContext.Alerts
                .OrderBy(a => a.Id)
                .Take(Constant.MAX_ALERT_COUNT)
                .Include(a => a.Sensor)
                    .ThenInclude(s => s.Device)
                        .ThenInclude(d => d.User)
                .Include(a => a.Sensor)
                    .ThenInclude(s => s.Device)
                        .ThenInclude(d => d.Location)
                .Include(a => a.Sensor)
                    .ThenInclude(s => s.SensorType)
                .Include(a => a.Log).ToList();

                return new JsonResult(alerts, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DateFormatString = "M/d/yyyy h:mm:ss tt"
                });
            }

            var alert = _appDbContext.Alerts
                .Where(a => a.Id == id)
                .Include(a => a.Log)
                .FirstOrDefault();

            return new JsonResult(alert);
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
    }
}
