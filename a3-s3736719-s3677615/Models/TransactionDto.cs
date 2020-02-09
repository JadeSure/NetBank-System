using System;
using System.ComponentModel.DataAnnotations;

namespace a3_s3736719_s3677615.Models
{
    public enum TransactionType
    {
        // Transaction Type: Deposit(D), Withdraw(W), Transfer(T), Service charge(S), BillPay(B)
        [Display(Name = "Deposit")]
        D = 1,
        [Display(Name = "Withdraw")]
        W = 2,
        [Display(Name = "Transfer")]
        T = 3,
        [Display(Name = "Service Charge")]
        S = 4,
        [Display(Name = "BillPay")]
        B = 5


    }

    public class TransactionDto
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        public int? DestinationAccountNumber { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }

    }
}
