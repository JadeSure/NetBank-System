using System;
using a3_s3736719_s3677615.Models;

namespace a3_s3736719_s3677615.ViewModels
{
    public class SearchHistoryViewModel
    {
        public int CustomerID { get; set; }
        public int AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public string Comment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
