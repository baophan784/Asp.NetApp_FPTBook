namespace FPTBookApp2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class author
    {
        public author()
        {
            this.products = new HashSet<product>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the author ID")]
        [Display(Name = "Author ID")]
        public string auID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the author name")]
        [Display(Name = "Author Name")]
        public string auName { get; set; }
        [Required(ErrorMessage = "Please enter the image of author")]
        [Display(Name = "Author Image")]
        public string auImage { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the description")]
        [Display(Name = "Author Description")]
        public string auDes { get; set; }
        public virtual ICollection<product> products { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
