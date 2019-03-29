using DeviceSM1.Data;
using DeviceSM1.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.Table
{
    [Table("Device")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int LocationId { get; set; }
        public string IMEI { get; set; }
        public string SIMCard { get; set; }
        public int DeviceTypeId { get; set; }
        public int Status { get; set; }
        public ApplicationUser User { get; set; }
        public IList<Sensor> Sensors { get; set; }
        public Location Location { get; set; }
        public DeviceType DeviceType { get; set; }

        //public Device()
        //{
        //    this.Sensors = new List<Sensor>(Constant.SENSOR_NUMBER);
        //    for (int i = 0; i < Constant.SENSOR_NUMBER; i++)
        //    {
        //        this.Sensors.Add(new Sensor() { SensorTypeId = i });
        //    }
        //}
    }
}
