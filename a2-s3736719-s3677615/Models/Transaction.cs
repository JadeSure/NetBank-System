using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace a2_s3736719_s3677615.Models
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

    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        // The relation between Account and Transaction is defined using Fluent API in DbContext class
        [Required]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("DestAccount")]
        public int? DestinationAccountNumber { get; set; }
        public virtual Account DestAccount { get; set; }

        [Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }

        // constructor
        public Transaction()
        {
        }

        public Transaction(TransactionType transactionType, int accountNumber,
            int destinationNumber, decimal amount, string comment, DateTime dateTime)
        {
            this.TransactionType = transactionType;
            this.AccountNumber = accountNumber;
            this.DestinationAccountNumber = destinationNumber;
            this.Amount = amount;
            this.Comment = comment;
            this.ModifyDate = dateTime;
        }
    }
}
