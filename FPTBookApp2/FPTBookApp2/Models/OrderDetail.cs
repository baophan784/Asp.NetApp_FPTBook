namespace FPTBookApp2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int ID { get; set; }
        public string ProID { get; set; }
        public int price { get; set; }
        public int qty { get; set; }
        public Nullable<int> OrderID { get; set; }
    
        public virtual product product { get; set; }
        public virtual Order Order { get; set; }
    }
}
