using System;
using System.ComponentModel.DataAnnotations;
using a3_s3736719_s3677615.Models;

namespace a3_s3736719_s3677615.ViewModels
{
    public class SearchHistoryViewModel
    {
        [Display(Name = "Customer ID")]
        public int? CustomerID { get; set; }

        [Display(Name = "Transaction Type")]
        public TransactionType? TransactionType { get; set; }

        [Display(Name = "Account Number")]
        public int? AccountNumber { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public decimal? MinAmount { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Amount should be greater than 0")]
        public decimal? MaxAmount { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

    }
}
