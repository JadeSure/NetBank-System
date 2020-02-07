using System;
using System.ComponentModel.DataAnnotations;

namespace a3_s3736719_s3677615.Models
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

    public class CustomerDto
    {
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

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

        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "PostCode is not valid, must be a 4 digit number.")]
        [StringLength(10)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Phone can not be empty.")]
        [RegularExpression(@"^\(61\)- [0-9]{4} [0-9]{4}$", ErrorMessage = "Phone is not valid, must in (61)- **** **** format.")]
        [StringLength(15)]
        public string Phone { get; set; }
    }

}
