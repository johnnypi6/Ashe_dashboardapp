using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeviceSM1.Mysqlconnect;
using System.Data;

namespace DeviceSM1.Controllers
{
    public class DeviceController : Controller
    {
        private ConDB conDB = new ConDB();
        [HttpGet]
        public IActionResult Index()
        {

            //$"SELECT id,user_id,location_id,IMEI,sim_card,vehicle,STATUS FROM device"
            //string selectIDquery = $"SELECT name FROM user WHERE id = '{user_id}'";
            //string selectLocationIDquery = $"SELECT name FROM location WHERE location = '{location_id}'";

            DataTable deviceInfo = conDB.GetData($"SELECT id,user_id,location_id,IMEI,sim_card,vehicle,STATUS FROM device");
            ViewData["deviceInfo"] = deviceInfo;
            return View();
        }
        public IActionResult addDevice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterDevice(string IMEI, string name, string sim_card, string location, string vehicle, int status,
          int p_no, int p_h, int p_l, int p_r, int t_no, int t_h, int t_l, int t_r,
          int h_no, int h_h, int h_l, int h_r, int m_no, int m_h, int m_l, int m_r,
          int l_no, int l_h, int l_l, int l_r, int c_no, int c_h, int c_l, int c_r)
        {
         

            string selectIDquery = $"SELECT id FROM user WHERE name = '{name}'";

            DataTable dt = conDB.GetData(selectIDquery);
            string user_id = dt.Rows[0]["id"].ToString();


            string selectLocationIDquery = $"SELECT id FROM location WHERE location = '{location}'";
            string locationID = dt.Rows[0]["id"].ToString();  


            string query = $"INSERT INTO device (user_id, location_id, IMEI, sim_card,  vehicle, status) " +
                                       $"VALUES ('{user_id}', '{location}','{IMEI}',  '{sim_card}', '{vehicle}', '{status}')";

            string queryp = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold,  status) " +
                   $"VALUES ( '1','pressure', '{p_no}', '{p_h}','{p_l}',  '{p_r}')";

            string queryt = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
                   $"VALUES ( '1','Temperature', '{t_no}', '{t_h}','{t_l}',  '{t_r}')";

            string queryh = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
                   $"VALUES ( '1','Humidity', '{h_no}', '{h_h}','{h_l}',  '{h_r}')";

            string querym = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
                   $"VALUES ( '1','Moisture', '{m_no}', '{m_h}','{m_l}',  '{m_r}')";

            string queryl = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
                   $"VALUES ( '1','Liquid Flow', '{l_no}', '{l_h}','{l_l}',  '{l_r}')";

            string queryc = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
                   $"VALUES ( '1','CO2', '{c_no}', '{c_h}','{c_l}',  '{c_r}')";
            conDB.ExecuteQuery(query);
               return RedirectToAction("create", "Customers", new { success = "true" });

           

        }
        

    }
}