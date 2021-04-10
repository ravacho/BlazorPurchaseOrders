using System;
using System.ComponentModel.DataAnnotations;
// This is the model for one row in the database table. You may need to make some adjustments.
namespace BlazorPurchaseOrders.Data
{
    public class Supplier
    {
        [Required]
        public int SupplierID { get; set; }
        [Required]
        [StringLength(50)]
        public string SupplierName { get; set; }
        [StringLength(50)]
        public string SupplierAddress1 { get; set; }
        [StringLength(50)]
        public string SupplierAddress2 { get; set; }
        [StringLength(50)]
        public string SupplierAddress3 { get; set; }
        [StringLength(10)]
        public string SupplierPostCode { get; set; }
        [StringLength(256)]
        public string SupplierEmail { get; set; }
        [Required]
        public bool SupplierIsArchived { get; set; }

    }
}

