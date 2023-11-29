namespace FPTBookApp2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public category()
        {
            this.products = new HashSet<product>();
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Category ID")]
        [Display(Name = "Category ID")]
        public string CatID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Category Name")]
        [Display(Name = "Category Name")]
        public string CatName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Category Description")]
        [Display(Name = "Category Description")]
        public string CatDes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
