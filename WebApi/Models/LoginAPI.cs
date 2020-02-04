using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using SimpleHashing;

namespace WebApi.Models
{
    public class LoginAPI
    {
        [Key, ForeignKey("Customer")]
        public int CustomerID { get; set; }
        //public virtual Customer Customer { get; set; }

        // string length need to be 50
        [Required, StringLength(8)]
        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }

        public LoginAPI()
        {
        }

        // change password in model
        public void ChangePassword(string oldPwd, string newPwd)
        {
            
            if (oldPwd == null || newPwd == null)
            {
                throw new ArgumentOutOfRangeException("Password cannot be empty.");
            }

            if (!PBKDF2.Verify(PasswordHash, oldPwd))
            {
                throw new InvalidOperationException("Password incorrect.");
            }

            if (oldPwd.Equals(newPwd))
            {
                throw new InvalidOperationException("New password cannot be the same as old one.");
            }

            PasswordHash = PBKDF2.Hash(newPwd);
            ModifyDate = DateTime.UtcNow;
        }

    }
}
