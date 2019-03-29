using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeviceSM1.Models;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DeviceSM1.Models.Identity;
using DeviceSM1.Data;
using Microsoft.EntityFrameworkCore;
using DeviceSM1.Models.Table;
using System.Security.Claims;
using System.Dynamic;
using DeviceSM1.Models.ViewModel;

namespace DeviceSM1.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _appDbContext;

        public DeviceController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IQueryable<Device> devices;

            if (User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
            {
                devices = _appDbContext.Devices
                    .Include(c => c.User)
                    .Include(c => c.Location)
                    .AsNoTracking();
            }
            else if (User.IsInRole("Customer"))
            {
                var id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                devices = _appDbContext.Devices
                   .Where(d => d.UserId == id)
                   .Include(d => d.User)
                   .Include(d => d.Location)
                   .AsNoTracking();
            }
            else
            {
                devices = Enumerable.Empty<Device>().AsQueryable();
            }

            return View(await devices.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var Users = await _userManager.GetUsersInRoleAsync("Customer");
            var DeviceTypes = await _appDbContext.DeviceTypes.ToListAsync();
            var Locations = await _appDbContext.Locations.Take(5).ToListAsync();

            dynamic model = new ExpandoObject();
            model.Users = Users;
            model.DeviceTypes = DeviceTypes;
            model.Locations = Locations;

            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var Device = await _appDbContext.Devices
                .Where(d => d.Id == id)
                .Include(d => d.User)
                .Include(d => d.Location)
                .Include(d => d.DeviceType)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.SensorType)
                .SingleAsync();
            var Users = await _userManager.GetUsersInRoleAsync("Customer");
            var DeviceTypes = await _appDbContext.DeviceTypes.ToListAsync();
            var Locations = await _appDbContext.Locations.Take(5).ToListAsync();

            dynamic model = new ExpandoObject();
            model.Device = Device;
            model.Users = Users;
            model.DeviceTypes = DeviceTypes;
            model.Locations = Locations;

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var device = _appDbContext.Devices
                .Where(d => d.Id == id)
                .Single();

            var result = _appDbContext.Devices.Remove(device);

            if (result.State == EntityState.Deleted) {
                await _appDbContext.SaveChangesAsync();
                return new JsonResult(new {
                    state = true
                });
            }

            return new JsonResult(new
            {
                state = false
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDevice(Device device)
        {
            var result = _appDbContext.Devices.Add(device);

            if (result.State == EntityState.Added)
            {
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> UpdateDevice(Device device)
        {
            if (ModelState.IsValid)
            {
                var newDevice = await _appDbContext.Devices
                    .Where(d => d.Id == device.Id)
                    .Include(d => d.Sensors)
                    .SingleAsync();

                newDevice.IMEI = device.IMEI;
                newDevice.UserId = device.UserId;
                newDevice.LocationId = device.LocationId;
                newDevice.SIMCard = device.SIMCard;
                newDevice.DeviceTypeId = device.DeviceTypeId;
                newDevice.Status = device.Status;
                
                for (int i = 0; i < Constant.SENSOR_NUMBER; i++)
                {
                    Sensor newSensor = newDevice.Sensors[i];
                    Sensor sensor = device.Sensors[i];

                    newSensor.SerialNumber = sensor.SerialNumber;
                    newSensor.HighThreshold = sensor.HighThreshold;
                    newSensor.LowThreshold = sensor.LowThreshold;
                    newSensor.RelayOperation = sensor.RelayOperation;
                }

                var result = _appDbContext.Devices.Update(newDevice);

                if (result.State == EntityState.Modified)
                {
                    await _appDbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            
            return RedirectToAction("Update", new { id = device.Id });
        }

        public async Task<IActionResult> Modal(int id)
        {
            var device = _appDbContext.Devices
                .Where(d => d.Id == id)
                .Include(d => d.User)
                .Include(d => d.Location)
                .Include(d => d.DeviceType)
                .Include(d => d.Sensors)
                    .ThenInclude(s => s.SensorType);
            
            return new JsonResult(await device.SingleAsync());
        }
    }
}