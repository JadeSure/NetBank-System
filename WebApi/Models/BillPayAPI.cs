﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Models
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

    public enum BillPayStatus
    {
        [Display(Name = "Ready")]
        Ready = 1,
        [Display(Name = "Blocked")]
        Blocked = 2
    }


    public class BillPayAPI
    {

        // Entity Framework
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BillPayID { get; set; }

        [ForeignKey("Account")]
        public int AccountNumber { get; set; }
        [JsonIgnore]
        public virtual AccountAPI Account { get; set; }

        [ForeignKey("Payee")]
        public int PayeeID { get; set; }
        [JsonIgnore]
        public virtual PayeeAPI Payee { get; set; }

        [Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public Period Period { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime ModifyDate { get; set; }
        public BillPayStatus BillPayStatus { set; get; }


        public BillPayAPI()
        {
        }
    }
}
