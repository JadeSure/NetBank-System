using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public enum State
    {
        // Account Type: Checking(C), Saving(S)
        [Display(Name = "Australian Capital Territory")]
        ACT = 1,
        [Display(Name = "New South Wales")]
        NSW = 2,
        [Display(Name = "Northern Territory")]
        NT = 3,
        [Display(Name = "Queensland")]
        QLD = 4,
        [Display(Name = "South Australia")]
        SA = 5,
        [Display(Name = "Tasmania")]
        TAS = 6,
        [Display(Name = "Victoria")]
        VIC = 7,
        [Display(Name = "Western Australia")]
        WA = 8

    }

    public class CustomerAPI
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Customer ID")]
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Customer Name can not be empty.")]
        [StringLength(50)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [StringLength(11)]
        public string TFN { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        public State State { get; set; }

        // must be 4 digital number
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "PostCode is not valid, must be a 4 digit number.")]
        [StringLength(10)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Phone can not be empty.")]
        [RegularExpression(@"^\(61\)- [0-9]{4} [0-9]{4}$", ErrorMessage = "Phone is not valid, must in (61)- **** **** format.")]
        [StringLength(15)]
        public string Phone { get; set; }

        public virtual List<AccountAPI> Accounts { get; set; }

        public CustomerAPI()
        {
        }

        public CustomerAPI(int customerID, string customerName, string tfn, string address, string city, string postCode, string phone)
        {
            CustomerID = customerID;
            CustomerName = customerName;
            TFN = tfn;
            Address = address;
            City = city;
            PostCode = postCode;
            Phone = phone;
        }

    }
}
