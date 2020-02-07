using System;
using System.ComponentModel.DataAnnotations;

namespace a3_s3736719_s3677615.Models
{
    public enum AccountType
    {
        // Account Type: Checking(C), Saving(S)
        [Display(Name = "Checking account")]
        C = 1,
        [Display(Name = "Saving account")]
        S = 2
    }

    public class AccountDto
    {
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Current Balance")]
        public decimal Balance { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifyDate { get; set; }

    }
}
