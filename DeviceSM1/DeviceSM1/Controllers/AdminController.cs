using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeviceSM1.Mysqlconnect;
using System.Data;

namespace DeviceSM1.Controllers
{
    public class AdminController : Controller
    {
        private ConDB conDB = new ConDB();

        [HttpGet]
        public IActionResult Index()
        {
            DataTable userInfo = conDB.GetData($"SELECT id, name, email,address,company,contactperson FROM user");
            ViewData["userInfo"] = userInfo;
            return View();
        }
      
        public IActionResult AddAdmin()
        {
            return View();
        }
        public IActionResult ComposeMail()
        {
            return View();
        }

       


        public IActionResult Delecte(int id)
        {
            DataTable user_Delect = conDB.GetData($"DELETE  FROM user WHERE id = {id};");
            ViewData["user_Delect"] = user_Delect;

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
                return RedirectToAction("index", "Customers", new { success = "true" });
            }
        }

        
       
    }
}