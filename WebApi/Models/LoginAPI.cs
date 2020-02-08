using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using SimpleHashing;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public enum LoginStatus
    {
        [Display(Name = "Active")]
        Active = 1,
        [Display(Name = "Locked")]
        Locked = 2
    }
    public class LoginAPI
    {
       

        [Key, ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual CustomerAPI Customer { get; set; }

        // string length need to be 50
        [Required, StringLength(8)]
        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }

        [Range(0, 3)]
        public int AttemptCount { get; set; }
        public LoginStatus LoginStatus { get; set; }

        public LoginAPI()
        {
        }
    }
}
