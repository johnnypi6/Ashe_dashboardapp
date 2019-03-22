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
    public class DeviceController : Controller
    {
        //private DBConnecter conDB = new DBConnecter();
        //[HttpGet]
        //public IActionResult index()
        //{
        //    string user_role = HttpContext.Session.GetString("role");

        //    if (user_role != "user")
        //    {
        //        DataTable device_Info = conDB.GetData($"SELECT id,user_id,location_id,IMEI,sim_card,vehicle,STATUS FROM device");
        //        ViewData["device_Info"] = device_Info;
        //            return View();
        //    }
        //    else {
        //        int id = Convert.ToInt32(HttpContext.Session.GetString("id"));
        //        DataTable device_Info = conDB.GetData($"SELECT id,user_id,location_id,IMEI,sim_card,vehicle,STATUS FROM device WHERE user_id = {id};");
        //        ViewData["device_Info"] = device_Info;
        //        return View();
        //    }
        //}

        //public IActionResult addDevice()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult RegisterDevice(string IMEI, string name, string sim_card, string location, string vehicle, int status,
        //  int p_no, int p_h, int p_l, int p_r, int t_no, int t_h, int t_l, int t_r,
        //  int h_no, int h_h, int h_l, int h_r, int m_no, int m_h, int m_l, int m_r,
        //  int l_no, int l_h, int l_l, int l_r, int c_no, int c_h, int c_l, int c_r)
        //{
         

        //    string selectIDquery = $"SELECT id FROM user WHERE name = '{name}'";
             
        //    DataTable dt = conDB.GetData(selectIDquery);
        //    string user_id = dt.Rows[0]["id"].ToString();


        //    //string selectLocationIDquery = $"SELECT id FROM location WHERE name = '{location}'";
        //    //DataTable dt1 = connector.GetData(selectLocationIDquery);
        //    //string locationID = dt1.Rows[0]["id"].ToString();  


        //    string query = $"INSERT INTO device (user_id, location_id, IMEI, sim_card,  vehicle, status) " +
        //                               $"VALUES ('{user_id}', '{location}','{IMEI}',  '{sim_card}', '{vehicle}', '{status}')";


        //    string selectDevice_id = $"SELECT id FROM device WHERE id like '%es'";
        //    DataTable dtDevice = conDB.GetData(selectDevice_id);
        //    string device_id = dtDevice.Rows[0]["id"].ToString();

        //    string queryp = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold,  status) " +
        //           $"VALUES ( '{device_id}','1', '{p_no}', '{p_h}','{p_l}',  '{p_r}')";

        //    string queryt = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
        //           $"VALUES ( '{device_id}','Temperature', '{t_no}', '{t_h}','{t_l}',  '{t_r}')";

        //    string queryh = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
        //           $"VALUES ( '{device_id}','Humidity', '{h_no}', '{h_h}','{h_l}',  '{h_r}')";

        //    string querym = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
        //           $"VALUES ( '{device_id}','Moisture', '{m_no}', '{m_h}','{m_l}',  '{m_r}')";

        //    string queryl = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
        //           $"VALUES ( '{device_id}','Liquid Flow', '{l_no}', '{l_h}','{l_l}',  '{l_r}')";

        //    string queryc = $"INSERT INTO sensor (device_id, type, serial_number, high_threshold, low_threshold, relay_operation, status) " +
        //           $"VALUES ( '{device_id}','CO2', '{c_no}', '{c_h}','{c_l}',  '{c_r}')";
        //    conDB.ExecuteQuery(query);
        //       return RedirectToAction("index", "Device", new { success = "true" });

           

        //}
        //public IActionResult Modal(int id)
        //{
        //    DataTable device_Info = conDB.GetData($"SELECT  IMEI, user_id, sim_card, location_id,vehicle, status FROM device WHERE id = {id};");


        //    DataTable sensor_Info = conDB.GetData($"SELECT id,type, serial_number,high_threshold,low_threshold,status FROM sensor WHERE device_id = {id};");

        //    return new JsonResult(new
        //    {
        //        device_Info = device_Info,
        //        sensor_Info = sensor_Info
        //    });
        //    //return View();
        //}


        //public IActionResult Delecte(int id)
        //{
        //    DataTable device_Delect = conDB.GetData($"DELETE  FROM device WHERE id = {id};");
        //    ViewData["device_Delect"] = device_Delect;

        //    return View();
        //}

    }
}