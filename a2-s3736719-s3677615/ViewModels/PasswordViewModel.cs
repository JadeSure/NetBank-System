using System;
using System.ComponentModel.DataAnnotations;
using a2_s3736719_s3677615.Models;

namespace a2_s3736719_s3677615.ViewModels
{

    // limitation for enter password
    public class PasswordViewModel
    {
        public Login Login { get; set; }

        [Required, Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required, Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }

}
