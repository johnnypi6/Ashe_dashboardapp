using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.Table
{
    [Table("Device")]
    public class TblDevice
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public string IMEI { get; set; }
        public string SIMCard { get; set; }
        public int Type { get; set; }
        public string Vehicle { get; set; }
        public int Status { get; set; }
    }
}
