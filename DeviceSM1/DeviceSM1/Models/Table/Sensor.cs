using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.Table
{
    [Table("Sensor")]
    public class Sensor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int DeviceId { get; set; }
        [Required]
        public int SensorTypeId { get; set; }
        public string SerialNumber { get; set; }
        public double HighThreshold { get; set; }
        public double LowThreshold { get; set; }
        public bool RelayOperation { get; set; }
        public int Status { get; set; }

        public Device Device { get; set; }
        public SensorType SensorType { get; set; }
    }
}
