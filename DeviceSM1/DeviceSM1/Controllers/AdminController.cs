using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeviceSM1.Models;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace DeviceSM1.Controllers
{
    public class AdminController : Controller
    {
        private DBConnecter connector = new DBConnecter();

        [HttpGet]
        public IActionResult Index()
        {
            string user_role = HttpContext.Session.GetString("role");
            int user_id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            if (user_role == "100")
            {
                DataTable userInfo = connector.GetData($"SELECT id, name, email, address, company, contactperson FROM user WHERE role = 'admin';");
                ViewData["userInfo"] = userInfo;
                return View();
            }
            else
            {
                return RedirectToAction("Profile", "Admin", new { id = user_id });
            }
        }
      
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult ComposeMail()
        {
            return View();
        }

        public IActionResult Profile(int id)
        {
            DataTable profile_Info = connector.GetData($"SELECT  name, email,password, address, company, contactperson,phone,mobile FROM user WHERE id = {id};");
            ViewData["profile_Info"] = profile_Info;
            return View();
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

        [HttpPost]
        public IActionResult RegisterAdmin(string name, string password, string company, string contactperson,
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
                                       $"VALUES ('{company}', '{contactperson}', '{address}', '{email}', '{phone}', '{mobile}', '{name}', '{password}', 'admin')";
                connector.ExecuteQuery(query);
                return RedirectToAction("index", "Admin", new { success = "true" });
            }
        }

        [HttpPost]
        public IActionResult UpdateAdmin(string name, string password, string company, string contactperson,
           string address, string email, string phone, string mobile)
        {
            string query = $"UPDATE user SET company='{company}', contactperson='{contactperson}', " +
                $"address='{address}', email='{email}', phone='{phone}', mobile='{mobile}' WHERE name='{name}'";
            connector.ExecuteQuery(query);
            return RedirectToAction("index", "Admin", new { success = "true" });
        }
    }
}