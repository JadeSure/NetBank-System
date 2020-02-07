using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace a3_s3736719_s3677615.Models
{
    public class AdminLogin
    {
        [Display(Name = "Admin ID"), Required]
        public string AdminId { set; get; }

        [Display(Name = "Admin Name"), Required]
        public string AdminName { set; get; }
    }
}
