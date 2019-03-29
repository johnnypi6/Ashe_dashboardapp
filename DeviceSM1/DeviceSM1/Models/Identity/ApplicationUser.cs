using DeviceSM1.Models.Table;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "username")]
        public override string UserName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "email")]
        public override string Email { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "phone")]
        public string Phone { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "mobile")]
        public string Mobile { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "address")]
        public string Address { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "company")]
        public string Company { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "contactperson")]
        public string ContactPerson { get; set; }
        public ICollection<Device>  Devices { get; set; }
    }
}
