using WebApi.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using a2_s3736719_s3677615.Utilities;
using System.Text.Json.Serialization;

namespace WebApi.Models
{


    public enum AccountType
    {
        // Account Type: Checking(C), Saving(S)
        [Display(Name = "Checking account")]
        C = 1,
        [Display(Name = "Saving account")]
        S = 2
    }

    public class AccountAPI
    {
        private const decimal WithdrawFee = 0.1m;
        private const decimal TransferFee = 0.2m;
        private const decimal CheckingOpeningBalance = 500;
        private const decimal CheckingMiniBalance = 200;
        private const decimal SavingOpeningBalance = 100;
        private const decimal SavingMiniBalance = 0;

        // autogerated unique ID for acount number
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Account Number")]
    
        // [Range(1000,9999)]
        public int AccountNumber { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        [Required, ForeignKey("Customer")]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        

        [Column(TypeName = "money"), DataType(DataType.Currency)]
        [Display(Name = "Current Balance")]
        public decimal Balance { get; set; }

        [Required, DataType(DataType.DateTime)]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifyDate { get; set; }


        //Navigational Property
        [JsonIgnore]
        public virtual CustomerAPI Customer { get; set; }
        public virtual List<BillPayAPI> BillPays { get; set; }
        public virtual List<TransactionAPI> Transactions { get; set; }

        
        }


    }



