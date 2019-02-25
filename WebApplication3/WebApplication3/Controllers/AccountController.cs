using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.MyClass;

namespace WebApplication3.Controllers
{    
    public class AccountController : Controller
    {
        private ConDB conDB = new ConDB();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            return RedirectToAction("Index", "Home");

            DataTable dt = conDB.GetData($"SELECT * FROM user WHERE name = '{username}';");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["password"].ToString() ==
                    EncodeString.MD5HashCryptography(password))
                {
                    HttpContext.Session.SetString("login", "1");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}