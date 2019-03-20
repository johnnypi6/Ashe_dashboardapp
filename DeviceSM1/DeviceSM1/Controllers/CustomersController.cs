using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DeviceSM1.Models;
using System.Data;

namespace DeviceSM1.Controllers
{
    public class CustomersController : Controller
    {
        private DBConnecter connector = new DBConnecter();

        public IActionResult Index()
        {  
            string user_role = HttpContext.Session.GetString("role");
            int user_id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            if (user_role != "user")
            {
                DataTable userInfo = connector.GetData($"SELECT id, name, email,address,company,contactperson FROM user WHERE role = 'user'");
                ViewData["userInfo"] = userInfo;
                return View();
            }
            else {
                return RedirectToAction("Profile", "Customers", new {id = user_id});
            }
            
        }

        public IActionResult Create(string success)
        {
            return View();
        }

        public IActionResult Profile(int id)
        {
          
            DataTable profile_Info = connector.GetData($"SELECT name, password, email, address, company, contactperson, phone, mobile FROM user WHERE id = {id};");
            ViewData["profile_Info"] = profile_Info;
            return View();
        }
        
        public IActionResult Modal(int id)
        {
            DataTable user_Info = connector.GetData( $"SELECT name, email, address, company, contactperson FROM user WHERE id = {id};");
            DataTable device_Info = connector.GetData($"SELECT id,location_id, IMEI,sim_card,vehicle,status FROM device WHERE user_id = {id};");
            return new JsonResult(new
            {
                user_Info = user_Info,
                device_Info = device_Info
            });
        }

        public IActionResult Update(int id)
        {
            DataTable profile_Info = connector.GetData($"SELECT id, name, email, password, address, company, contactperson, phone, mobile FROM user WHERE id = {id};");
            ViewData["profile_Info"] = profile_Info;
            return View();
        }

        public IActionResult Delete(int id)
        {
            connector.ExecuteQuery($"DELETE FROM user WHERE id = {id};");

            return new JsonResult(new
            {
                success = "success"
            });
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
                HttpContext.Session.SetString("role", "100");                
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = connector.GetData($"SELECT * FROM user WHERE name = '{username}';");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["password"].ToString() ==
                    EncodeString.EncodeTo64(password))
                {
                    var role = dt.Rows[0]["role"];
                    var id = dt.Rows[0]["id"];

                    HttpContext.Session.SetString("login", "1");
                    HttpContext.Session.SetString("username", username);
                    HttpContext.Session.SetString("role", role.ToString());
                    HttpContext.Session.SetString("id", id.ToString());

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(string name, string password, string company, string contactperson,
            string address, string email, string phone, string mobile)
        {
            password = EncodeString.EncodeTo64(password);
            DataTable dt = connector.GetData($"SELECT * FROM user WHERE name = '{name}';");
            if (dt.Rows.Count > 0)
            {
                return RedirectToAction("create", "Customers", new { success = "false" });
            }
            else
            {
                string query = $"INSERT INTO user (company, contactperson, address, email, phone, mobile, name, password, role) " +
                                       $"VALUES ('{company}', '{contactperson}', '{address}', '{email}', '{phone}', '{mobile}', '{name}', '{password}', 'user')";
                connector.ExecuteQuery(query);
                return RedirectToAction("index", "Customers", new { success = "true" });
            }
        }

        [HttpPost]
        public IActionResult UpdateUser(string name, string password, string company, string contactperson,
            string address, string email, string phone, string mobile)
        {
            string query = $"UPDATE user SET company='{company}', contactperson='{contactperson}', " +
                $"address='{address}', email='{email}', phone='{phone}', mobile='{mobile}' WHERE name='{name}'";
            connector.ExecuteQuery(query);
            return RedirectToAction("index", "Customers", new { success = "true" });
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