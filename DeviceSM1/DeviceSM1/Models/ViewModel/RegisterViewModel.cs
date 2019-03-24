using DeviceSM1.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceSM1.Models.ViewModel
{
    public class RegisterViewModel : ApplicationUser
    {
        [Required]
        [DataType(DataType.Password)]
        //[DataType(DataType.Text)]
        [Display(Name = "password")]
        public string Password { get; set; }
    }
}
