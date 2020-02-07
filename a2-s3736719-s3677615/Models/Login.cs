using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using a2_s3736719_s3677615.Utilities;
using SimpleHashing;

namespace a2_s3736719_s3677615.Models
{
    public enum LoginStatus
    {
        Active = 1,
        Locked = 2
    }
    public class Login
    {
        [Key, ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Required, StringLength(8)]
        public string UserID { get; set; }

        [Required, StringLength(64)]
        public string PasswordHash { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }

        [Range(0, 3)]
        public int AttemptCount { get; set; }

        public LoginStatus LoginStatus { get; set; }

        public Login()
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

        public void AttemptLogin(string pwd, DateTime loginTime)
        {
            if (LoginStatus == LoginStatus.Locked
                && DateTime.Compare(ModifyDate.AddMinutes(1), loginTime) <= 0)
            {
                UnlockUser(loginTime);
            }

            // Check if the account has been locked
            if (LoginStatus == LoginStatus.Locked)
            {
                throw new InvalidOperationException("Account locked, please try later.");
            }

            // Check if the login attempt exceed 3 times
            if (AttemptCount == 3)
            {
                LockUser(loginTime);
                throw new InvalidOperationException("You've reached attempt limit, please try later.");
            }

            // Check if password valid
            if (!PBKDF2.Verify(PasswordHash, pwd))
            {
                AttemptCount++;
                throw new InvalidOperationException("Login failed, you have " + (3 - AttemptCount) + " attempts left.");
            }
        }


        public void UnlockUser(DateTime unlockTime)
        {
            LoginStatus = LoginStatus.Active;
            ModifyDate = unlockTime;
            AttemptCount = 0;
        }

        public void LockUser(DateTime lockTime)
        {
            LoginStatus = LoginStatus.Locked;
            ModifyDate = lockTime;
        }


    }
}