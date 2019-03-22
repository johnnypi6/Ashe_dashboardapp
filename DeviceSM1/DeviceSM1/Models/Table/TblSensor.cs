using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.Table
{
    [Table("Sensor")]
    public class TblSensor
    {
        [Key]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int Type { get; set; }
        public string SerialNumber { get; set; }
        public string HighThreshold { get; set; }
        public string LowThreshold { get; set; }
        public int RelayOperation { get; set; }
        public int Status { get; set; }
    }
}
