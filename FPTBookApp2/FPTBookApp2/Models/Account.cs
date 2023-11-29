namespace FPTBookApp2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public partial class Account
    {
        public Account()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Username")]
        [Display(Name = "Username")]
        [StringLength(maximumLength: 30, MinimumLength = 1, ErrorMessage = "Length must be between 1 to 30")]
        public string AccID { get; set; }
        [Display(Name = "Password")]
        [StringLength(maximumLength: 200, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 25")]
        public string pass { get; set; }

       

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Full name")]
        [Display(Name = "Full Name")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Length must be between 5 to 30")]
        public string fullname { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Please enter the Email")]
        public string email { get; set; }
        [Display(Name = "Telephone")]
        [Required(ErrorMessage = "Please enter the Tel")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string tel { get; set; }
        public Nullable<int> state { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        [NotMapped]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
