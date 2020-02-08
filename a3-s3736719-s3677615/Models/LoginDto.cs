using System;
using System.Collections.Generic;
using System.Linq;
using SimpleHashing;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations.Schema;

namespace a3_s3736719_s3677615.Models
{
    //[JsonConverter(typeof(StringEnumConverter))]
    public enum LoginStatus
    {
        [Display(Name = "Active")]
        Active = 1,
        [Display(Name = "Locked")]
        Locked = 2
    }
    public class LoginDto
    {
        [Key, ForeignKey("Customer")]
        [Display (Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Required, StringLength(8)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required, DataType(DataType.DateTime)]
        [Display (Name = "Modify Date")]
        public DateTime ModifyDate { get; set; }

        [Range(0, 3)]
        [Display(Name = "Attemp Count")]
        public int AttemptCount { get; set; }

        [Display(Name = "Login Status")]
        public LoginStatus LoginStatus { get; set; }
    }
}
