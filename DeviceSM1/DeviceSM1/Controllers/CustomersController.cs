using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DeviceSM1.Mysqlconnect;
using System.Data;

namespace DeviceSM1.Controllers
{
    public class CustomersController : Controller
    {
        private ConDB conDB = new ConDB();

        public IActionResult Index()
        {
            DataTable userInfo = conDB.GetData($"SELECT id, name, email,address,company,contactperson FROM user");
            ViewData["userInfo"] = userInfo;
            if (ChkLogin() == true)
            {
                string username = HttpContext.Session.GetString("username");
                //ViewData["success"] = success;
                ViewData["username"] = username;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customers");
            }
        }


        public IActionResult Create(string success)
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
        public IActionResult Modal(int id)
        {
            DataTable user_Info = conDB.GetData( $"SELECT  name, email, address, company, contactperson FROM user WHERE id = {id};");

            DataTable device_Info = conDB.GetData($"SELECT id,location_id, IMEI,sim_card,vehicle,status FROM device WHERE user_id = {id};");

            return new JsonResult(new
            {
                user_Info = user_Info,
                device_Info = device_Info
            });
            ////return View();
        }


        public IActionResult Delecte(int id)
        {
            DataTable user_Delect = conDB.GetData($"DELETE  FROM user WHERE id = {id};");
            ViewData["user_Delect"] = user_Delect;

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            string superpassword = "superadmin";
            string superusername = "superadmin";

            if (password == superpassword && username == superusername)
            {
                HttpContext.Session.SetString("login", "1");
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = conDB.GetData($"SELECT * FROM user WHERE name = '{username}';");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["password"].ToString() ==
                    EncodeString.EncodeTo64(password))
                {
                    HttpContext.Session.SetString("login", "1");
                    HttpContext.Session.SetString("username", username);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(string company, string contactperson,
            string address, string email, string phone, string mobile, string name,
            string password)
        {
            //password = EncodeString.EncodeTo64(password);
            DataTable dt = conDB.GetData($"SELECT * FROM user WHERE name = '{name}';");
            if (dt.Rows.Count > 0)
            {
                return RedirectToAction("create", "Customers", new { success = "false" });
            }
            else
            {
                string query = $"INSERT INTO user (company, contactperson, address, email, phone, mobile, name, password, role) " +
                                       $"VALUES ('{company}', '{contactperson}', '{address}', '{email}', '{phone}', '{mobile}', '{name}', '{password}', 'user')";
                conDB.ExecuteQuery(query);
                return RedirectToAction("create", "Customers", new { success = "true" });
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
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