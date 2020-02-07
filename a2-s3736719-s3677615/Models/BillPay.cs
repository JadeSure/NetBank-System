using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace a2_s3736719_s3677615.Models
{
    public enum Period
    {
        // Period: Monthly(M), Quarterly(Q), Annually(Y), Once off (S)
        [Display(Name = "Monthly")]
        M = 1,
        [Display(Name = "Quarterly")]
        Q = 2,
        [Display(Name = "Annually")]
        Y = 3,
        [Display(Name = "Once")]
        S = 4
    }


    public class BillPay
    {
        // Entity Framework
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("Payee")]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public Period Period { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }

        public BillPay()
        {
        }
    }
}
