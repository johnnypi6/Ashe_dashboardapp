using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.Table
{
    [Table("Alert")]
    public class Alert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int SensorId { get; set; }
        [Required]
        public int LogId { get; set; }

        public Sensor Sensor { get; set; }
        public Log Log { get; set; }
    }
}
