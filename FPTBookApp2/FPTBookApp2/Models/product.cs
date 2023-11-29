using System.Web;
namespace FPTBookApp2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class product
    {
        public product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }


        [Display(Name = "Product ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Product ID")]
        public string ProID { get; set; }
        [Display(Name = "Product Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Product Name")]
        public string ProName { get; set; }
        [Display(Name = "Image")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Product Image")]
        public string ProImage { get; set; }
        [Display(Name = "Price")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Product Price")]
        public int ProPrice { get; set; }
        [Display(Name = "Quantity")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Quantity")]
        public int ProQty { get; set; }
        [Display(Name = "Product Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Product Description")]
        public string ProDes { get; set; }
        public string CatID { get; set; }
        public string auID { get; set; }
        public virtual author author { get; set; }
        public virtual category category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase myfile { get; set; }
    }
}
